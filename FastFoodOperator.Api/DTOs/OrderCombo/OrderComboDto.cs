using FastFoodOperator.Api.DTOs.Ingredient;
using FastFoodOperator.Api.Entities;

namespace FastFoodOperator.Api.DTOs.OrderCombo
{
	public class OrderComboDto
	{
		public int Quantity { get; set; }
		public required string ComboName { get; set; } = string.Empty!;
		public List<string> Ingredients { get; set; } = [];
	}
}
