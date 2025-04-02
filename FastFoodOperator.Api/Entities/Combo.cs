namespace FastFoodOperator.Api.Entities
{
    public class Combo
    {
        public int Id { get; set; }
        public ICollection<ComboProduct> ComboProducts { get; set; } = [];

        public required string Name { get; set; }
        public decimal BasePrice { get; set; }
	}   
}
