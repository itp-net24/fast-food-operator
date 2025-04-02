namespace FastFoodOperator.Api.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        
        public Category? Category { get; set; }
        public ICollection<ComboProduct> ComboProducts { get; set; } = [];
        public ICollection<ProductIngredient> ProductIngredients { get; set; } = [];
        
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
    }
}
