namespace FastFoodOperator.Api.DTOs.Product;

public class ProductUpdateDto
{
	public int Id { get; init; }
	public string? Name { get; init; }
	public string? Description { get; init; }
	public decimal? BasePrice { get; init; }
	public ProductIngredientUpdateDto[] Ingredients { get; init; } = [];
	public string? PictureUrl { get; init; }
}