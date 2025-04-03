namespace FastFoodOperator.Api.DTOs
{
	public class AddOrderDto
	{
		public string CustomerNote { get; set; } = string.Empty!;
		public ICollection<OrderComboDto> OrderComboDtos { get; set; } = [];
		public ICollection<OrderProductDto> OrderProductDtos { get; set; } = [];
	}
}
