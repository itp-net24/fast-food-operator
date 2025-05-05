using FastFoodOperator.Api.DTOs.Ingredient;
using FastFoodOperator.Api.DTOs.ProductVariant;
using FastFoodOperator.Api.DTOs.Tags;

namespace FastFoodOperator.Api.DTOs.Product;

public class ProductResponseDto
{
	public int Id { get; init; }
	public string Name { get; init; } = string.Empty;
	public string? Description { get; init; }
	public decimal BasePrice { get; init; }
	public string? ImageUrl { get; init; }
	public TagResponseDto[] Tags { get; set; } = [];
	public ProductVariantResponseDto[] Variants { get; init; } = [];
	public IngredientResponseDto[] Ingredients { get; init; } = [];
}
