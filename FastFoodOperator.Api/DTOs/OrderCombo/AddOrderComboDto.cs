using FastFoodOperator.Api.DTOs.Combo;

namespace FastFoodOperator.Api.DTOs.OrderCombo
{
	public class AddOrderComboDto
	{
		public ComboMinimalResponseDto ComboMinimalResponseDto { get; set; } = null!;
		public int Quantity { get; set; }
		public decimal BasePrice { get; set; }
		

	}
}
