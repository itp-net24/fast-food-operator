namespace FastFoodOperator.Api.DTOs.Product;

public class ProductMinimalResponseDto
{
	public int Id { get; init; }
	public string? PictureUrl { get; init; }
	public string Name { get; init; } = string.Empty;
	public decimal BasePrice { get; init; }
}


