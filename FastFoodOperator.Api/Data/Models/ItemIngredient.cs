namespace FastFoodOperator.Api.Data.Models
{
    public class ItemIngredient
    {
        public int? IngredientId { get; set; }
        public int? ItemId { get; set; }
        public required bool Requried { get; set; }
    }


}
