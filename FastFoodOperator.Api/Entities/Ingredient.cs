namespace FastFoodOperator.Api.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public ICollection<ProductIngredient> ProductIngredients { get; } = [];

        public string Name { get; set; } = null!;
        public decimal PriceModifier { get; set; }
    }
}
