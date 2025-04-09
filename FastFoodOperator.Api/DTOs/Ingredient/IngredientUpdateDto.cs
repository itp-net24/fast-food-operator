using System.ComponentModel.DataAnnotations;

namespace FastFoodOperator.Api.DTOs.Ingredient;

public class IngredientUpdateDto
{
	[Required]
	[Range(0, int.MaxValue, ErrorMessage = "The ingredient id must be a positive number.")]
	public int Id { get; set; }
	public string? Name { get; set; }
	public decimal? PriceModifier { get; set; }
}