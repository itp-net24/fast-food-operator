using System.ComponentModel.DataAnnotations;

namespace FastFoodOperator.Api.DTOs.Ingredient;

public class IngredientCreateDto
{
	[Required]
	[MinLength(3, ErrorMessage = "The ingredient name must be at least 3 characters long.")]
	public string Name { get; set; } = null!;
	
	[Required]
	public decimal PriceModifier { get; set; }
}