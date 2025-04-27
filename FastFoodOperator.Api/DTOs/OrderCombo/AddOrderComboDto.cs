namespace FastFoodOperator.Api.DTOs.OrderCombo
{
	public class AddOrderComboDto
	{
		public int ComboId { get; set; }
		public required string ComboName { get; set; }
		public decimal FinalPrice { get; set; }

		public int Quantity { get; set; }

	}
}
