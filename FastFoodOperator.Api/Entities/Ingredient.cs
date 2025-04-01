namespace FastFoodOperator.Api.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public ICollection<ProductIngredient> ProductIngredients { get; } = [];
        
        public required string Name { get; set; }
        public decimal PriceModifier { get; set; }
    }
}
