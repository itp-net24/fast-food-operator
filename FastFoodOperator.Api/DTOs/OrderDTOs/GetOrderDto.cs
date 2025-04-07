using FastFoodOperator.Api.DTOs.OrderComboDTOs;
using FastFoodOperator.Api.DTOs.OrderProductDTOs;

namespace FastFoodOperator.Api.DTOs.OrderDTOs
{
	public class GetOrderDto
	{
		public int OrderId { get; set; }
		public int OrderNumber { get; set; }
		public bool OrderStatus { get; set; }
		public string CustomerNote { get; set; } = null!;
		public DateTime CreatedAt { get; set; }
		public ICollection<OrderProductDto> OrderProducts { get; set; } = [];
		public ICollection<OrderComboDto> OrderCombos { get; set; } = [];
	}
}
