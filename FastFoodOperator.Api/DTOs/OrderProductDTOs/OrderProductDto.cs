namespace FastFoodOperator.Api.DTOs.OrderProductDTOs
{
	public class OrderProductDto
	{
		public int ProductId { get; set; }
		public required string ProductName { get; set; } = string.Empty;
		public int Quantity { get; set; }
	}
}
