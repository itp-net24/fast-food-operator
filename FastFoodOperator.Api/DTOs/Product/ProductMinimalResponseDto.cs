namespace FastFoodOperator.Api.DTOs.Product
{
	public class ProductMinimalResponseDto
	{
		public int? ProductVariantId { get; set; }
		public int ProductId { get; set; }
		public int[] IngredientsId { get; set; } = [];
		public int Quantity { get; set; }
	}
}
