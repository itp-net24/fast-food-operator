namespace FastFoodOperator.Api.Entities
{
    public class ProductIngredient
    {
        public int IngredientId { get; set; }
        public int ProductId { get; set; }
        
        public Product Product { get; set; } = null!;
        public Ingredient Ingredient { get; set; } = null!;
        
        public bool Required { get; set; }
        
        
        public override bool Equals(object? obj)
        {
	        return Equals(obj as ProductIngredient);
        }
        
        public bool Equals(ProductIngredient? other)
        {
	        if (other is null) return false;
	        if (ReferenceEquals(this, other)) return true;

	        return ProductId == other.ProductId &&
	               IngredientId == other.IngredientId &&
	               Required == other.Required;
        }
        
        public override int GetHashCode()
        {
	        return HashCode.Combine(ProductId, IngredientId, Required);
        }
    }
}
