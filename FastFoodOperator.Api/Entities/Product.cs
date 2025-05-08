namespace FastFoodOperator.Api.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public string? ImageUrl { get; set; }
        
        public ICollection<ProductTag> Tags { get; set; } = [];
        
        
        public int? DefaultVariantId { get; set; }
        public ICollection<ProductVariant> Variants { get; set; } = [];
        
        public ICollection<ComboProduct> ComboProducts { get; set; } = [];
        public ICollection<ProductIngredient> ProductIngredients { get; set; } = [];
    }
}
