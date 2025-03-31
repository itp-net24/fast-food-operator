using System.ComponentModel.DataAnnotations.Schema;

namespace FastFoodOperator.Api.Data
{
    public partial class AppDbContext
    {
        public class ComboProduct
        {
            public int ComboId { get; set; }
            public int ProductId { get; set; }
            public int ProductVariantId { get; set; }


            [ForeignKey(nameof(ComboId))]
            public Combo Combo { get; set; } = null!;

            [ForeignKey(nameof(ProductId))]
            public Product Product { get; set; } = null!;

            [ForeignKey(nameof(ProductVariantId))]
            public ProductVariant ProductVariant { get; set; } = null!;
        }
	}
}
