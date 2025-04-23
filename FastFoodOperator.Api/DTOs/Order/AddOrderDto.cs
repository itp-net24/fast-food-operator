using FastFoodOperator.Api.DTOs.OrderCombo;
using FastFoodOperator.Api.DTOs.OrderProduct;

namespace FastFoodOperator.Api.DTOs.Orders
{
	public class AddOrderDto
	{
		public string CustomerNote { get; set; } = string.Empty!;
		public ICollection<AddOrderComboDto> OrderComboDtos { get; set; } = [];
		public ICollection<AddOrderProductDto> OrderProductDtos { get; set; } = [];
	}
}
