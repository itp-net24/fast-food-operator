namespace FastFoodOperator.Api.Entities
{
    public class ComboProduct : IEquatable<ComboProduct>
    {
        public int ComboId { get; set; }
        public int ProductId { get; set; }
        
        public int? ProductVariantId { get; set; }

        public Combo Combo { get; set; } = null!;
        public Product Product { get; set; } = null!;

        public ProductVariant? ProductVariant { get; set; }


        public override bool Equals(object? obj)
        {
	        return Equals(obj as ComboProduct);
        }
        
        public bool Equals(ComboProduct? other)
        {
	        if (other is null) return false;
	        if (ReferenceEquals(this, other)) return true;

	        return ComboId == other.ComboId &&
	               ProductId == other.ProductId &&
	               ProductVariantId == other.ProductVariantId;
        }
        
        public override int GetHashCode()
        {
	        return HashCode.Combine(ComboId, ProductId, ProductVariantId);
        }
    }
}
