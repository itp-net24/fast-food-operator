namespace FastFoodOperator.Api.DTOs.Combo
{
	public class ComboMinimalResponseDto
	{
		public required string ComboName { get; set; }
		public decimal BasePrice { get; set; }
		public decimal PriceModifier { get; set; }
		public int ComboId { get; set; }
	}
}
