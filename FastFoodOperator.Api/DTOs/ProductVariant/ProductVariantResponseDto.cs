namespace FastFoodOperator.Api.DTOs.ProductVariant;

public class ProductVariantResponseDto
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public decimal PriceModifier { get; set; }
	
	public int ProductId { get; set; }
}