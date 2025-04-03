using FastFoodOperator.Api.Data;
using FastFoodOperator.Api.DTOs;
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
			try {

				var order = new Order
				{
					OrderNumber = await GenerateOrderNumber(),
					CustomerNote = orderDto.CustomerNote,
					OrderStatus = false,
				};

				await _context.Orders.AddAsync(order);
				await _context.SaveChangesAsync();

				order.OrderProducts = orderDto.OrderProductDtos.Select(op => new OrderProduct
				{
					ProductId = op.ProductId,
					Quantity = op.Quantity,
					OrderId = order.Id
				}).ToList();

				order.OrderCombos = orderDto.OrderComboDtos.Select(oc => new OrderCombo
				{
					ComboId = oc.ComboId,
					Quantity = oc.Quantity,
					OrderId = order.Id
				}).ToList();

				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error adding order");
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
				_logger.LogError(ex, "Failed to get orders");
				return new GetOrderDto();
			}
		}

		public async Task CompleteOrder(int orderId)
		{
			var order = await _context.Orders.FindAsync(orderId);

			if (order == null)
			{
				throw new KeyNotFoundException($"Order with ID {orderId} not found.");
			}

			order.OrderStatus = true;
			await _context.SaveChangesAsync();
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
			var today = DateTime.Today;

			var maxOrderNumber = await _context.Orders
				.Where(o => o.CreatedAt.Date == today)
				.MaxAsync(o => (int?)o.OrderNumber);

			return maxOrderNumber.HasValue ? maxOrderNumber.Value + 1 : 1000;
		}

	}
}
