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

		public async Task AddOrder(AddOrderDto orderDto)
		{
			await using var transaction = await _context.Database.BeginTransactionAsync();
			try {

				var order = new Order
				{
					OrderNumber = await GenerateOrderNumber(),
					CustomerNote = orderDto.CustomerNote,
				};

				await _context.Orders.AddAsync(order);

				order.OrderProducts = (orderDto.OrderProductDtos ?? new List<AddOrderProductDto>())
					.Select(op => new OrderProduct
				{
					ProductId = op.ProductId,
					Quantity = op.Quantity,
					OrderId = order.Id
				}).ToList();

				order.OrderCombos = (orderDto.OrderComboDtos ?? new List<AddOrderComboDto>())
					.Select(oc => new OrderCombo
				{
					ComboId = oc.ComboId,
					Quantity = oc.Quantity,
					OrderId = order.Id
				}).ToList();

				await _context.SaveChangesAsync();
				await transaction.CommitAsync();
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
					.Include(o => o.OrderProducts)
						.ThenInclude(op => op.Product)
					.Include(o => o.OrderCombos)
						.ThenInclude(oc => oc.Combo)
					.FirstOrDefaultAsync(o => o.Id == orderId);

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
				var orders = await _context.Orders
					.AsNoTracking()
					.IncludeFullOrder()
					.OrderByDescending(o => o.CompletedAt)
					.Take(10)
					.ToListAsync();

				var ordersDto = orders.Select(o => o.OrderToOrderDto()).ToList();

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
