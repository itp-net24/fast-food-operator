namespace FastFoodOperator.Api.Data.Models
{
    public class Ingredient
    {   
        public int Id { get; set; }
        public required string Name { get; set; }
        public required decimal PriceModefier { get; set; }
        public List<Product> Products { get; } = [];
        public List<ProductIngredient> ProductIngredients { get; } = [];

    }
}
