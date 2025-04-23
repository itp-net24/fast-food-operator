namespace FastFoodOperator.Api.DTOs.Combo;

public class ComboUpdateDto
{
	public int Id { get; init; }
	public string? Name { get; init; }
	public decimal? BasePrice { get; init; }
	public ComboProductsUpdateDto[] Products { get; init; } = [];
}