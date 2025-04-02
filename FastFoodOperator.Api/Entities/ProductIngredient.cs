namespace FastFoodOperator.Api.Entities
{
    public class ProductIngredient
    {
        public int IngredientId { get; set; }
        public int ProductId { get; set; }
        
        public Product Product { get; set; } = null!;
        public Ingredient Ingredient { get; set; } = null!;
        
        public bool Required { get; set; }
    }
}
