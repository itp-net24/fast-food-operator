namespace FastFoodOperator.Api.Entities
{
    public class Combo
    {
        public int Id { get; set; }
<<<<<<< HEAD
        public ICollection<ComboProduct> ComboProducts { get; set; } = [];
        public string Name { get; set; } = string.Empty;
=======
        public string Name { get; set; } = null!;
>>>>>>> develop
        public decimal BasePrice { get; set; }
        public string? ImageUrl { get; set; }
        
        public int? MainComboProductId { get; set; }
        
        public ICollection<ComboProduct> ComboProducts { get; set; } = [];
        public ICollection<ComboGroup> ComboGroups { get; set; } = [];
	}   
}
