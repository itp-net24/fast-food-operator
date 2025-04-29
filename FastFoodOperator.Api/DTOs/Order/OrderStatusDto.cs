using FastFoodOperator.Api.Entities;

namespace FastFoodOperator.Api.DTOs.Orders
{
	public class OrderStatusDto
	{
		public int OrderNumber { get; set; }
		public OrderStatus OrderStatus { get; set; }
	}
}
