using FastFoodOperator.Api.DTOs.Combo;
using FastFoodOperator.Api.DTOs.Ingredient;
using FastFoodOperator.Api.Entities;

namespace FastFoodOperator.Api.DTOs.OrderCombo
{
	public class OrderComboDto
	{
		
		public required string ComboName { get; set; } = string.Empty!;
		public int Quantity { get; set; }
		public List<string> Ingredients { get; set; } = [];
	}
}
