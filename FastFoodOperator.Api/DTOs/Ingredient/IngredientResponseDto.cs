namespace FastFoodOperator.Api.DTOs.Ingredient;

public class IngredientResponseDto
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public decimal PriceModifier { get; set; }
}