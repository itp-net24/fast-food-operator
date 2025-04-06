using FastFoodOperator.Api.DTOs.Category;

namespace FastFoodOperator.Api.DTOs.Product;

public class ProductCategoryResponseDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal BasePrice { get; init; }

    public required CategoryResponseDto CategoryResponseDto { get; init; }
}