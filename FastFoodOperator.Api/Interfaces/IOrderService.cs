using FastFoodOperator.Api.DTOs.OrderDTOs;

namespace FastFoodOperator.Api.Interfaces
{
	public interface IOrderService
	{
		public Task AddOrder(AddOrderDto orderDto);
		public Task<GetOrderDto> GetOrder(int orderId);
		public Task CompleteOrder(CompleteOrderDto order);
		public Task DeleteOrder(int orderId);
		public Task<int> GenerateOrderNumber();
	}
}
