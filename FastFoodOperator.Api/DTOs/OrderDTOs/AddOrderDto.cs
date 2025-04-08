using FastFoodOperator.Api.DTOs.OrderComboDTOs;
using FastFoodOperator.Api.DTOs.OrderProductDTOs;

namespace FastFoodOperator.Api.DTOs.OrderDTOs
{
	public class AddOrderDto
	{
		public string CustomerNote { get; set; } = string.Empty!;
		public ICollection<AddOrderComboDto> OrderComboDtos { get; set; } = [];
		public ICollection<AddOrderProductDto> OrderProductDtos { get; set; } = [];
	}
}
