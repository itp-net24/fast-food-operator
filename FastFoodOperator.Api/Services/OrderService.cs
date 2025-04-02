using FastFoodOperator.Api.Data;
using FastFoodOperator.Api.Entities;

namespace FastFoodOperator.Api.Services
{
	public class OrderService
	{
		private readonly AppDbContext _context;
		public OrderService(AppDbContext context)
		{
			_context = context;
		}

		//public async Task AddOrder()
		//{
			//var orderNumber = _context.Orders.
		//}

		public async Task AddOrderComboAsync(OrderCombo orderCombo)
		{
			await _context.AddAsync(orderCombo);
		}

		public async Task GetOrderComboAsync()
		{

		}

		public async Task AddOrderProductAsync(OrderProduct orderProduct)
		{
			await _context.AddAsync(orderProduct);
		}



	}
}
