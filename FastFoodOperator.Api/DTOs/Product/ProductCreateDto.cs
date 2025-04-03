using System.ComponentModel.DataAnnotations;

namespace FastFoodOperator.Api.DTOs.Product;

public class ProductCreateDto
{
	[Required]
	[MinLength(3, ErrorMessage = "Combo name must be at least 3 characters long.")]	
	public required string Name { get; init; }
	
	public string? Description { get; init; }
	
	[Required]
	[Range(0, int.MaxValue, ErrorMessage = "Category id must be a non-negative integer.")]
	public int CategoryId { get; init; }
	
	[Required]
	[Range(0, int.MaxValue, ErrorMessage = "Base price must be a non-negative value.")]	
	public decimal BasePrice { get; init; }

	public ProductIngredientCreateDto[] Ingredients { get; init; } = [];
}