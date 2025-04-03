using System.ComponentModel.DataAnnotations;

namespace FastFoodOperator.Api.DTOs;

public class ProductIngredientCreateDto
{
	[Required]
	[Range(0, int.MaxValue, ErrorMessage = "Product id must be a non-negative integer.")]
	public int IngredientId { get; init; }
	
	public bool IsRequired { get; init; }
}