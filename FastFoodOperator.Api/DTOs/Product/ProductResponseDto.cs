using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace FastFoodOperator.Api.DTOs.Product;

public class ProductResponseDto
{
	public int Id { get; init; }
	public string Name { get; init; } = string.Empty;
	public string? Description { get; init; }
	public decimal BasePrice { get; init; }
	public int CategoryId { get; init; }
	public string? PictureUrl { get; init; }
}
