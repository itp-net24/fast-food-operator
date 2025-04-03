using FastFoodOperator.Api.Services;

namespace FastFoodOperator.Api.DTOs;

public class ProductUpdateDto
{
	public int Id { get; init; }
	public string? Name { get; init; }
	public string? Description { get; init; }
	public decimal? BasePrice { get; init; }
	public ProductIngredientUpdateDto[] Ingredients { get; init; } = [];
}