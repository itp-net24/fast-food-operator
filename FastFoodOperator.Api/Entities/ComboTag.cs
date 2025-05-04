namespace FastFoodOperator.Api.Entities
{
	public class ComboTag
	{
		public int ComboId { get; set; }
		public Combo Combo { get; set; } = null!;

		public int TagId { get; set; }
		public Tag Tag { get; set; } = null!;
	}

}
