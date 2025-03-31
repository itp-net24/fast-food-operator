namespace FastFoodOperator.Api.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal BasePrice { get; set; }
        public List<Ingredient>? Ingredients { get;}
        public List<ProductIngredient>? ItemIngredients { get; }
        public required Category ItemCategory {get;set;}
    }


}
