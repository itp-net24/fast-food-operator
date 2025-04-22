namespace FastFoodOperator.Api.Entities
{
    public class Combo
    {
        public int Id { get; set; }
        public ICollection<ComboProduct> ComboProducts { get; set; } = [];

        public string Name { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
	}   
}
