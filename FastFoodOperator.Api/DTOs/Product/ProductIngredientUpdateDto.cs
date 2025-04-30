using System.ComponentModel.DataAnnotations;

namespace FastFoodOperator.Api.DTOs.Product;

public class ProductIngredientUpdateDto
{
	[Required]
	[Range(0, int.MaxValue, ErrorMessage = "Ingredient id must be a non-negative integer.")]
	public int IngredientId { get; init; }
	public bool Required { get; init; }
}