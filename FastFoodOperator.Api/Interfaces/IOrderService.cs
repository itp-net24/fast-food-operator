using FastFoodOperator.Api.DTOs;

namespace FastFoodOperator.Api.Interfaces
{
	public interface IOrderService
	{
		public Task AddOrder(AddOrderDto orderDto);
		public Task<GetOrderDto> GetOrder(int orderId);
		public Task CompleteOrder(int orderId);
		public Task DeleteOrder(int orderId);
		public Task<int> GenerateOrderNumber();
	}
}
