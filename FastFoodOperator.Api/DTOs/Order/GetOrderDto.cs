using FastFoodOperator.Api.DTOs.OrderCombo;
using FastFoodOperator.Api.DTOs.OrderProduct;
using FastFoodOperator.Api.Entities;

namespace FastFoodOperator.Api.DTOs.Orders
{
	public class GetOrderDto
	{
		public int OrderId { get; set; }
		public int OrderNumber { get; set; }
		public OrderStatus OrderStatus { get; set; }
		public string CustomerNote { get; set; } = null!;
		public DateTime CreatedAt { get; set; }
		public ICollection<OrderProductDto> OrderProducts { get; set; } = [];
		public ICollection<OrderComboDto> OrderCombos { get; set; } = [];
	}
}
