using FastFoodOperator.Api.DTOs.OrderComboDTOs;
using FastFoodOperator.Api.DTOs.OrderProductDTOs;

namespace FastFoodOperator.Api.DTOs.OrderDTOs
{
	public class GetOrderDto
	{
		public int OrderNumber { get; set; }
		public DateTime CreatedAt { get; set; }
		public ICollection<OrderProductDto> OrderProducts { get; set; } = [];
		public ICollection<OrderComboDto> OrderCombos { get; set; } = [];
	}
}
