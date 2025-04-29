namespace FastFoodOperator.Api.Entities
{
    public class Combo
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal BasePrice { get; set; }
        public string? ImageUrl { get; set; }
        
        public int? MainComboProductId { get; set; }
        
        public ICollection<ComboProduct> ComboProducts { get; set; } = [];
        public ICollection<ComboGroup> ComboGroups { get; set; } = [];
	}   
}
