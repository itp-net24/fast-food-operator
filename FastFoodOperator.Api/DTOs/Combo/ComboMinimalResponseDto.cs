using FastFoodOperator.Api.DTOs.Product;

namespace FastFoodOperator.Api.DTOs.Combo
{
	public class ComboMinimalResponseDto
	{
		public List<ProductMinimalResponseDto> Products { get; set; } = [];
		public int ComboId { get; set; }
		public int Quantity { get; set; }

	}
}

