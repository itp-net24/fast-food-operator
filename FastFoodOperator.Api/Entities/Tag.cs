namespace FastFoodOperator.Api.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal TaxRate { get; set; }

        public ICollection<ProductTag> ProductTags { get; set; } = [];
		public ICollection<ComboTag> ComboTags { get; set; } = [];
	}
}
