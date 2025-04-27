namespace FastFoodOperator.Api.DTOs.Product
{
	public class ProductMinimalResponseDto
	{
		public string? ProductVariant { get; set; } = null!;
		public decimal ProductVariantPriceModifier { get; set; }
		public int ProductId { get; set; }
	}
}
