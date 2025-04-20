using FastFoodOperator.Api.DTOs.Product;

namespace FastFoodOperator.Api.DTOs.Combo;

public class ComboDetailedResponseDto
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public decimal BasePrice { get; set; }

	public ProductResponseDto[] Products { get; set; } = [];
}