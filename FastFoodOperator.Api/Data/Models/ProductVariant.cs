using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFoodOperator.Api.Data.Models
{
    public class ProductVariant
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }


        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;


        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal PriceModifier { get; set; }
    }
}
