namespace FastFoodOperator.Api.DTOs;

public class ProductResponseDto
{
	public int Id { get; init; }
	public string Name { get; init; } = string.Empty;
	public string? Description { get; init; }
	public decimal BasePrice { get; init; }
}