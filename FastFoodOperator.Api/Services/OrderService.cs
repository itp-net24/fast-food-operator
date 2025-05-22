using FastFoodOperator.Api.Data;
using FastFoodOperator.Api.DTOs;
using FastFoodOperator.Api.DTOs.Combo;
using FastFoodOperator.Api.DTOs.OrderCombo;
using FastFoodOperator.Api.DTOs.OrderProduct;
using FastFoodOperator.Api.DTOs.Orders;
using FastFoodOperator.Api.DTOs.Product;
using FastFoodOperator.Api.Entities;
using FastFoodOperator.Api.Hubs;
using FastFoodOperator.Api.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FastFoodOperator.Api.Services
{
	public class OrderService : IOrderService
	{
		private readonly AppDbContext _context;
		private readonly IHubContext<OrderHub> _hub;
		private readonly ILogger<OrderService> _logger;
		public OrderService(AppDbContext context, IHubContext<OrderHub> hub, ILogger<OrderService> logger)
		{
			_context = context;
			_hub = hub;
			_logger = logger;
		}

		public async Task<OrderReceiptDto> AddOrder(AddOrderDto orderDto)
		{
			await using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				var productIds = orderDto.OrderProductDtos?
					.Select(op => op.ProductMinimalResponseDto?.ProductId ?? 0)
					.ToList() ?? new List<int>();

				var comboIds = orderDto.OrderComboDtos?
					.Select(oc => oc.ComboMinimalResponseDto.ComboId)
					.ToList() ?? new List<int>();

				var comboProductsId = orderDto.OrderComboDtos?
					.SelectMany(oc => oc.ComboMinimalResponseDto.Products)
					.Select(p => p.ProductId)
					.ToList() ?? new List<int>();

				var existingComboProducts = await _context.Products
					.Where(p => comboProductsId.Contains(p.Id))
					.Include(p => p.ProductIngredients)
						.ThenInclude(pi => pi.Ingredient)
					.Include(p => p.Tags)
						.ThenInclude(pt => pt.Tag)
					.Include(p => p.Variants)
					.ToListAsync();

				var existingProducts = await _context.Products
					.Where(p => productIds.Contains(p.Id))
					.Include(p => p.Tags)
						.ThenInclude(pt => pt.Tag)
					.Include(p => p.ProductIngredients)
						.ThenInclude(pi => pi.Ingredient)
					.Include(p => p.Variants)
					.ToListAsync();

				var existingCombos = await _context.Combos
					.Where(c => comboIds.Contains(c.Id))
					.Include(c => c.Tags)
						.ThenInclude(ct => ct.Tag)
					.ToListAsync();

				var missingProductIds = productIds
					.Except(existingProducts.Select(p => p.Id))
					.ToList();

				var missingComboIds = comboIds
					.Except(existingCombos.Select(c => c.Id))
					.ToList();

				if (missingProductIds.Any() || missingComboIds.Any())
				{
					var errorMessage = "";

					if (missingProductIds.Any())
						errorMessage += $"Invalid Product IDs: {string.Join(", ", missingProductIds)}. ";

					if (missingComboIds.Any())
						errorMessage += $"Invalid Combo IDs: {string.Join(", ", missingComboIds)}.";

					throw new Exception(errorMessage.Trim());
				}

				var order = new Order
				{
					OrderNumber = await GenerateOrderNumber(),
					CustomerNote = orderDto.CustomerNote,
					OrderStatus = OrderStatus.Created,
				};

				var vatSixPercent = 0m;
				var vatTwelvePercent = 0m;
				var vatTwentyfivePercent = 0m;

				var receiptProducts = new List<ProductReceiptDto>();
				var receiptCombos = new List<ComboReceiptDto>();

				await _context.Orders.AddAsync(order);

				order.OrderProducts = (orderDto.OrderProductDtos ?? new List<AddOrderProductDto>())
					.Select(op =>
					{
						var product = existingProducts.FirstOrDefault(p => p.Id == op.ProductMinimalResponseDto.ProductId);
						if (product == null)
							throw new Exception($"Product with ID {op.ProductMinimalResponseDto.ProductId} not found.");

						var variant = product.Variants.FirstOrDefault(v => v.Id == op.ProductMinimalResponseDto.ProductVariantId);

						var ingredients = product.ProductIngredients
							.Where(pi => op.ProductMinimalResponseDto.IngredientsId.Contains(pi.IngredientId))
							.Select(pi => pi.Ingredient)
							.ToList();

						var ingredientPriceModifierSum = ingredients.Sum(i => i.PriceModifier);

						var vatRate = product.Tags.Any() ? product.Tags.Max(pt => pt.Tag.TaxRate) : 0m;

						var productName = variant == null ? product.Name : $"{product.Name} - {variant.Name}";
						decimal baseTotal = (product.BasePrice + (variant?.PriceModifier ?? 0) + ingredientPriceModifierSum) * op.ProductMinimalResponseDto.Quantity;

						var vatAmount = baseTotal * ((vatRate - 1)/ vatRate);
						
						if (vatRate == 1.06m)
							vatSixPercent += vatAmount;
						else if (vatRate == 1.12m)
							vatTwelvePercent += vatAmount;
						else if (vatRate == 1.25m)
							vatTwentyfivePercent += vatAmount;


						decimal finalPrice = baseTotal;

						receiptProducts.Add(new ProductReceiptDto { 
							ProductName = productName,
							ProductPrice = baseTotal, 
							Quantity = op.ProductMinimalResponseDto.Quantity 
						});

						return new OrderProduct
						{
							ProductName = productName,
							Quantity = op.ProductMinimalResponseDto.Quantity,
							OrderId = order.Id,
							FinalPrice = finalPrice,
							Ingredients = ingredients.Select(i => i.Name).ToList()
						};
					})
					.ToList();

				order.OrderCombos = (orderDto.OrderComboDtos ?? new List<AddOrderComboDto>())
					.Select(oc =>
					{
						var combo = existingCombos.FirstOrDefault(o => o.Id == oc.ComboMinimalResponseDto.ComboId);
						if (combo == null)
						{
							throw new Exception($"Combo with Id {oc.ComboMinimalResponseDto.ComboId} not found");
						}

						var productNames = new List<string>();
						var productAllocations = new List<(decimal originalPrice, decimal vatRate)>();
						decimal fullOriginalPriceSum = 0;
						decimal additionalPrices = 0;

						foreach (var p in oc.ComboMinimalResponseDto.Products)
						{
							var product = existingComboProducts.First(prod => prod.Id == p.ProductId);
							var variant = product.Variants.FirstOrDefault(v => v.Id == p.ProductVariantId);

							var ingredients = product.ProductIngredients?
								.Where(pi => p.IngredientsId.Contains(pi.IngredientId))
								.Select(pi => pi.Ingredient)
								.ToList() ?? new List<Ingredient>();

							var vatRate = product.Tags.Any() ? product.Tags.Max(pt => pt.Tag.TaxRate) : 0m;
							var ingredientPriceModifierSum = ingredients.Sum(i => i.PriceModifier);

							decimal originalPrice = (product.BasePrice + (variant?.PriceModifier ?? 0) + ingredientPriceModifierSum) * p.Quantity;

							productAllocations.Add((originalPrice, vatRate));
							fullOriginalPriceSum += originalPrice;

							additionalPrices += ((variant?.PriceModifier ?? 0) + ingredientPriceModifierSum) * p.Quantity;

							string ingredientNames = string.Join(", ", ingredients.Select(i => i.Name));
							string productName = variant == null
								? $"{product.Name} - {ingredientNames}"
								: $"{product.Name} - {variant.Name} - {ingredientNames}";

							productNames.Add(productName);
						}

						var finalPrice = (combo.BasePrice + additionalPrices) * oc.ComboMinimalResponseDto.Quantity;

						for (int i = 0; i < productAllocations.Count; i++)
						{
							var (originalPrice, vatRate) = productAllocations[i];
							var share = originalPrice / fullOriginalPriceSum;
							var allocatedPrice = finalPrice * share;

							var vatAmount = allocatedPrice * ((vatRate - 1) / vatRate);

							if (vatRate == 1.06m)
								vatSixPercent += vatAmount;
							else if (vatRate == 1.12m)
								vatTwelvePercent += vatAmount;
							else if (vatRate == 1.25m)
								vatTwentyfivePercent += vatAmount;
						}

						receiptCombos.Add(new ComboReceiptDto
						{
							ComboName = combo.Name,
							ComboPrice = finalPrice,
							Quantity = oc.ComboMinimalResponseDto.Quantity
						});

						return new OrderCombo
						{
							ComboName = combo.Name,
							Quantity = oc.ComboMinimalResponseDto.Quantity,
							OrderId = order.Id,
							Products = string.Join(", ", productNames),
							FinalPrice = finalPrice
						};
					})
					.ToList();

				
				await _context.SaveChangesAsync();
				await transaction.CommitAsync();

				await _hub.Clients.All.SendAsync("ReceiveOrder", order.OrderToOrderDto());
				
				var vat6Rounded = Math.Round(vatSixPercent, 2);
				var vat12Rounded = Math.Round(vatTwelvePercent, 2);
				var vat25Rounded = Math.Round(vatTwentyfivePercent, 2);
				
				var totalTax = vat6Rounded + vat12Rounded + vat25Rounded;

				var orderFinalPrice = Math.Round(receiptProducts.Sum(p => p.ProductPrice) + receiptCombos.Sum(c => c.ComboPrice), 2);
				
				return new OrderReceiptDto
				{
				    Products = receiptProducts,
				    Combos = receiptCombos,
				    OrderNumber = order.OrderNumber,
				    CreatedAt = order.CreatedAt,
				    TotalVatSixPercent = vat6Rounded,
				    TotalVatTwelvePercent = vat12Rounded,
				    TotalVatTwentyFivePercent = vat25Rounded,
				    TotalTax = totalTax,
					OrderFinalPrice = orderFinalPrice,
					CustomerNote = order.CustomerNote
				};
				
								
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error adding order");

				await transaction.RollbackAsync();
				throw;
			}
		}
		public async Task<GetOrderDto> GetOrder(int orderId)
		{
			try
			{
				var order = await _context.Orders
					.Where(o => o.Id == orderId)
					.Include(o => o.OrderProducts)
					.Include(o => o.OrderCombos)
					.FirstOrDefaultAsync();

				if (order == null)
				{
					_logger.LogWarning($"Order with ID {orderId} not found.");
					return new GetOrderDto();
				}

				var orderDto = order.OrderToOrderDto();

				return orderDto;

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to get order");
				return new GetOrderDto();
			}
		}
		public async Task<List<GetOrderDto>> GetOrders()
		{
			try
			{
				var oneMinuteAgo = DateTime.UtcNow.AddMinutes(-1);

				var completedOrders = await _context.Orders
					.Include(o => o.OrderProducts)
					.Include(o => o.OrderCombos)
					.AsNoTracking()
					.Where(o => o.OrderStatus == OrderStatus.Completed && o.CompletedAt >= oneMinuteAgo)
					.OrderByDescending(o => o.CompletedAt)
					.Take(10)
					.ToListAsync();

				var nonCompletedOrders = await _context.Orders
					.Include(o => o.OrderProducts)
					.Include(o => o.OrderCombos)
					.AsNoTracking()
					.Where(o => o.OrderStatus != OrderStatus.Completed)
					.ToListAsync();

				var combinedOrders = completedOrders
					.Concat(nonCompletedOrders)
					.ToList();

				var ordersDto = combinedOrders.Select(o => o.OrderToOrderDto()).ToList();

				return ordersDto;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to retrieve orders");
				throw;
			}
		}
		public async Task<GetOrdernumbersDto> DisplayOrderNumbers() 
		{
			try
			{
				var threeMinutesAgo = DateTime.UtcNow.AddMinutes(-3);

				var orders = await _context.Orders
					.Where(o => o.OrderStatus != OrderStatus.Completed || (o.OrderStatus == OrderStatus.Completed && o.CompletedAt >= threeMinutesAgo)).ToListAsync();

				var orderNumbersDto = new GetOrdernumbersDto
				{
					Orders = orders.Select(o => new OrderStatusDto
				{
						OrderNumber = o.OrderNumber,
						OrderStatus = o.OrderStatus
					}).ToList()
				};

				return orderNumbersDto;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to display ordernumbers");
				throw;
			}
		}

		public async Task CompleteOrder(UpdateOrderDto orderDto)
		{
			try 
			{
				var order = await _context.Orders.FindAsync(orderDto.OrderId);

				if (order == null)
				{
					throw new KeyNotFoundException($"Order with ID {orderDto.OrderId} not found.");
				}

				order.OrderStatus = OrderStatus.Completed;
				order.CompletedAt = DateTime.UtcNow;
				await _context.SaveChangesAsync();
				
				await _hub.Clients.All.SendAsync("CompleteOrder", order.Id, order.OrderStatus);
			} 
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Failed to complete order with order ID {orderDto.OrderId}");
			}
		}

		public async Task StartOrder(UpdateOrderDto orderDto)
		{
			try
			{
				var order = await _context.Orders.FindAsync(orderDto.OrderId);
				
				if (order == null)
				{
					throw new KeyNotFoundException($"Order with ID {orderDto.OrderId} not found");
				}

				if (order.OrderStatus == OrderStatus.Completed)
				{
					_logger.LogError($"Order {order.Id} is already completed");
					return;
				}

				order.OrderStatus = OrderStatus.InProgress;
				order.StartedAt = DateTime.UtcNow;
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Failed to update order with order ID {orderDto.OrderId}");
			}
		}

		public async Task DeleteOrder(int orderId)
		{
			var order = await _context.Orders.FindAsync(orderId);

			if (order == null)
			{
				throw new KeyNotFoundException($"Order with ID {orderId} not found.");
			}

			_context.Orders.Remove(order);
			await _context.SaveChangesAsync();
			
			await _hub.Clients.All.SendAsync("RemoveOrder", orderId);
		}
		public async Task<int> GenerateOrderNumber()
		{
			var todayLocal = DateTime.Today;
			var tomorrowLocal = todayLocal.AddDays(1);

			var timeZone = TimeZoneInfo.Local; 
			var todayUtc = TimeZoneInfo.ConvertTimeToUtc(todayLocal, timeZone);
			var tomorrowUtc = TimeZoneInfo.ConvertTimeToUtc(tomorrowLocal, timeZone);

			var maxOrderNumber = await _context.Orders
				.Where(o => o.CreatedAt >= todayUtc && o.CreatedAt < tomorrowUtc)
				.MaxAsync(o => (int?)o.OrderNumber);

			return maxOrderNumber.HasValue ? maxOrderNumber.Value + 1 : 1000;

		}

	}
}
