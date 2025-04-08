namespace FastFoodOperator.Api.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        
        public Category? Category { get; set; }
        public ICollection<ComboProduct> ComboProducts { get; set; } = [];
        public ICollection<ProductIngredient> ProductIngredients { get; set; } = [];

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }

        public string? PictureUrl { get; set; } = string.Empty;
    }
}
