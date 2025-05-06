namespace FastFoodOperator.Api.DTOs.Combo
{
	public class ComboReceiptDto
	{
		public required string ComboName { get; set; }
		public decimal ComboPrice { get; set; }
		public int Quantity { get; set; }
	}
}
