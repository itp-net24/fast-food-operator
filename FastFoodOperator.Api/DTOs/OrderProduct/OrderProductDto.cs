using FastFoodOperator.Api.DTOs.Ingredient;

namespace FastFoodOperator.Api.DTOs.OrderProduct
{
	public class OrderProductDto
	{
<<<<<<< HEAD
		public required string ProductName { get; set; } = string.Empty;
		public int Quantity { get; set; }
		public List<string>? Ingredients { get; set; } = [];
=======
		public int ProductId { get; set; }
		public required string ProductName { get; set; } = string.Empty;
		public int Quantity { get; set; }
		public List<string> Ingredients { get; set; } = [];
>>>>>>> develop
	}
}
