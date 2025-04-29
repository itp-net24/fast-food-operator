namespace FastFoodOperator.Api.DTOs.ProductVariant;

public class ProductVariantResponseDto
{
	public int Id { get; set; }
<<<<<<< HEAD
	public int ProductId { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
	public decimal PriceModifier { get; set; }
=======
	public string Name { get; set; } = null!;
	public decimal PriceModifier { get; set; }
	
	public int ProductId { get; set; }
>>>>>>> develop
}