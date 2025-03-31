namespace FastFoodOperator.Api.Data.Models
{
    public class ProductIngredient
    {
        public int? IngredientId { get; set; }
        public int? ProductId { get; set; }
        public required bool Requried { get; set; }

        public Product Product { get; set; } = null!;
        public Ingredient Ingredient { get; set; } = null!;
    }
}
