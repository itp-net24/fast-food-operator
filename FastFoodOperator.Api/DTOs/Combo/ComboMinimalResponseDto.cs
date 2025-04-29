using FastFoodOperator.Api.DTOs.Product;

namespace FastFoodOperator.Api.DTOs.Combo
{
	public class ComboMinimalResponseDto
	{
		public List<AddOrderProductMinimalResponseDto> Products { get; set; } = [];
		public int ComboId { get; set; }
		public int Quantity { get; set; }

	}
}

