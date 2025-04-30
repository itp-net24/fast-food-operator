using FastFoodOperator.Api.DTOs.Orders;

namespace FastFoodOperator.Api.Interfaces
{
	public interface IOrderService
	{
		public Task<int> AddOrder(AddOrderDto orderDto);
		public Task<GetOrderDto> GetOrder(int orderId);
		public Task<GetOrdernumbersDto> DisplayOrderNumbers();
		public Task CompleteOrder(UpdateOrderDto order);
		public Task DeleteOrder(int orderId);
		public Task<int> GenerateOrderNumber();
		public Task<List<GetOrderDto>> GetOrders();
		public Task StartOrder(UpdateOrderDto order);
	}
}
