namespace FastFoodOperator.Api.DTOs.OrderProduct
{
	public class AddOrderProductDto
	{
		public required string ProductName { get; set; }
		public int Quantity { get; set; }
		public string? ProductVariant { get; set; } = null!;
		public List<string>? ProductIngredient { get; set; } = new List<string>();
		public decimal FinalPrice { get; set; }
	}
}
