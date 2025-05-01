using FastFoodOperator.Api.DTOs.Tags;
using FastFoodOperator.Api.Entities;

namespace FastFoodOperator.Api.DTOs.Product;

public class ProductMinimalResponseDto
{
	public int Id { get; init; }
	public string Name { get; init; } = string.Empty;
	public TagResponseDto[] Tags { get; set; } = [];
	public decimal BasePrice { get; init; }
	public string? ImageUrl { get; init; }
}
