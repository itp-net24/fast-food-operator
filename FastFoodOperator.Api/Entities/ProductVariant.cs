namespace FastFoodOperator.Api.Entities
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        
        public Product Product { get; set; } = null!;

        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal PriceModifier { get; set; }
    }
}
