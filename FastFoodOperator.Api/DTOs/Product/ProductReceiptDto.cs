namespace FastFoodOperator.Api.DTOs.Product
{
	public class ProductReceiptDto
	{
		public required string ProductName { get; set; }
		public decimal ProductPrice { get; set; }
		public int Quantity { get; set; }
	}
}
