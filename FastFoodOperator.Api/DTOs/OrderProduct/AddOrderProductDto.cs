using FastFoodOperator.Api.DTOs.Product;

namespace FastFoodOperator.Api.DTOs.OrderProduct
{
	public class AddOrderProductDto
	{
		public ProductMinimalResponseDto ProductMinimalResponseDto { get; set; } = null!;
	}
}
