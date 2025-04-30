using FastFoodOperator.Api.Data;
using FastFoodOperator.Api.DTOs;
using FastFoodOperator.Api.DTOs.OrderCombo;
using FastFoodOperator.Api.DTOs.OrderProduct;
using FastFoodOperator.Api.DTOs.Orders;
using FastFoodOperator.Api.Entities;
using FastFoodOperator.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FastFoodOperator.Api.Services
{
	public class OrderService : IOrderService
	{
		private readonly AppDbContext _context;
		private readonly ILogger<OrderService> _logger;
		public OrderService(AppDbContext context, ILogger<OrderService> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<int> AddOrder(AddOrderDto orderDto)
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
					.ToListAsync();

				var existingProducts = await _context.Products
					.Where(p => productIds.Contains(p.Id))
					.ToListAsync();

				var existingCombos = await _context.Combos
					.Where(c => comboIds.Contains(c.Id))
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

				var productVariantIds = orderDto.OrderProductDtos?
					.Select(op => op.ProductMinimalResponseDto.ProductVariantId)
					.OfType<int>()
					.ToList() ?? new List<int>();

				var comboProductVariantIds = orderDto.OrderComboDtos?
					.SelectMany(oc => oc.ComboMinimalResponseDto.Products
						.Select(p => p.ProductVariantId)
						.OfType<int>())
					.ToList() ?? new List<int>();

				var ingredientIds = orderDto.OrderProductDtos?
					.SelectMany(op => op.ProductMinimalResponseDto.IngredientsId) 
					.ToList() ?? new List<int>();

				var comboIngredientIds = orderDto.OrderComboDtos?
					.SelectMany(oc => oc.ComboMinimalResponseDto.Products)
					.SelectMany(p => p.IngredientsId)                      
					.ToList() ?? new List<int>();

				var existingProductVariant = await _context.ProductVariants
					.Where(pv => productVariantIds.Contains(pv.Id))
					.ToListAsync();

				var comboExistingProductVariant = await _context.ProductVariants
					.Where(pv => comboProductVariantIds.Contains(pv.Id))
					.ToListAsync();

				var existingIngredients = await _context.Ingredients
					.Where(pi => ingredientIds.Contains(pi.Id)) 
					.ToListAsync();

				var comboExistingIngredients = await _context.Ingredients
					.Where(pi => ingredientIds.Contains(pi.Id))
					.ToListAsync();

				var order = new Order
				{
					OrderNumber = await GenerateOrderNumber(),
					CustomerNote = orderDto.CustomerNote,
					OrderStatus = OrderStatus.Created,
				};

				await _context.Orders.AddAsync(order);

				order.OrderProducts = (orderDto.OrderProductDtos ?? new List<AddOrderProductDto>())
					.Select(op =>
					{
						var product = existingProducts.FirstOrDefault(p => p.Id == op.ProductMinimalResponseDto.ProductId);
						if (product == null)
							throw new Exception($"Product with ID {op.ProductMinimalResponseDto.ProductId} not found.");

						var variant = existingProductVariant.FirstOrDefault(v => v.Id == op.ProductMinimalResponseDto.ProductVariantId);

						var ingredients = existingIngredients.Where(i => op.ProductMinimalResponseDto.IngredientsId.Contains(i.Id)).ToList();
						var ingredientPriceModifierSum = ingredients.Sum(i => i.PriceModifier);

						return new OrderProduct
						{
							ProductName = variant == null ? product.Name : $"{product.Name} - {variant.Name}",
							Quantity = op.ProductMinimalResponseDto.Quantity,
							OrderId = order.Id,
							FinalPrice = (product.BasePrice + (variant?.PriceModifier ?? 0) + ingredientPriceModifierSum) * op.ProductMinimalResponseDto.Quantity,
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
						decimal additionalPrices = 0;

						foreach (var p in oc.ComboMinimalResponseDto.Products)
				{
							var product = existingComboProducts.FirstOrDefault(prod => prod.Id == p.ProductId);

							if (product == null)
				{
								throw new Exception($"Product with ID {p.ProductId} not found.");
							}

							var variant = comboExistingProductVariant.FirstOrDefault(v => v.Id == p.ProductVariantId);

							var ingredients = comboExistingIngredients
								.Where(i => p.IngredientsId.Contains(i.Id))
								.ToList();

							
							string productName = variant == null
								? product.Name
								: $"{product.Name} - {variant.Name}";


							productNames.Add(productName);

							additionalPrices += (variant?.PriceModifier ?? 0) + ingredients.Sum(i => i.PriceModifier);
						}


						var finalPrice = (combo.BasePrice + additionalPrices) * oc.ComboMinimalResponseDto.Quantity;

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

				return order.OrderNumber;
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
				var threeMinutesAgo = DateTime.UtcNow.AddMinutes(-3);

				var completedOrders = await _context.Orders
					.Include(o => o.OrderProducts)
					.Include(o => o.OrderCombos)
					.AsNoTracking()
					.Where(o => o.OrderStatus == OrderStatus.Completed && o.CompletedAt >= threeMinutesAgo)
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
