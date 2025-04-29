namespace FastFoodOperator.Api.DTOs.Ingredient;

public class IngredientResponseDto
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public decimal PriceModifier { get; set; }
}