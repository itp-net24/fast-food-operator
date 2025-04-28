namespace FastFoodOperator.Api.Entities
{
	public class OrderProduct
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public Order Order { get; set; } = null!;
		public required string ProductName { get; set; }
		public decimal FinalPrice { get; set; }
		public int Quantity { get; set; }
		public List<string>? ProductIngredients { get; set; } = new List<string>();
	}
}
