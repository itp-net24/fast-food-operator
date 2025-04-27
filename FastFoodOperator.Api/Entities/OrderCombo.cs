namespace FastFoodOperator.Api.Entities
{

	public class OrderCombo
	{
		public int OrderComboId { get; set; }
		public int OrderId { get; set; }
		public Order Order { get; set; } = null!;
		public required string ComboName { get; set; }
		public decimal FinalPrice { get; set; }
		public int Quantity { get; set; }
	}
}
