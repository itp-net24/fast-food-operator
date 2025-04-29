namespace FastFoodOperator.Api.Entities
{

	public class OrderCombo
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public Order Order { get; set; } = null!;
		public required string ComboName { get; set; }
<<<<<<< HEAD
=======
		public string Products { get; set; } = string.Empty!;
>>>>>>> develop
		public decimal FinalPrice { get; set; }
		public int Quantity { get; set; }
	}
}
