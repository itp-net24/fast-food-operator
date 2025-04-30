namespace FastFoodOperator.Api.Entities
{
    public class ComboProduct
    {
	    public int Id { get; set; }
	    
	    public int? ComboId { get; set; }
	    public Combo? Combo { get; set; }
	    
	    public int? ComboGroupId { get; set; }
	    public ComboGroup? ComboGroup { get; set; }

	    public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        
        public int? DefaultVariantId { get; set; }
        public ProductVariant? DefaultProductVariant { get; set; }
    }
}