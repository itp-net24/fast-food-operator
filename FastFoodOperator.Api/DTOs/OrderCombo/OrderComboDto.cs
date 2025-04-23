using FastFoodOperator.Api.DTOs.Ingredient;
using FastFoodOperator.Api.Entities;

namespace FastFoodOperator.Api.DTOs.OrderCombo
{
	public class OrderComboDto
	{
		public int ComboId { get; set; }
		public int Quantity { get; set; }
		public required string ComboName { get; set; } = string.Empty!;
		public List<IngredientDto> Ingredients { get; set; } = [];
	}
}
