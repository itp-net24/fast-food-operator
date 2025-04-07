namespace FastFoodOperator.Api.DTOs.OrderComboDTOs
{
	public class OrderComboDto
	{
		public int ComboId { get; set; }
		public int Quantity { get; set; }
		public required string ComboName { get; set; } = string.Empty!;

	}
}
