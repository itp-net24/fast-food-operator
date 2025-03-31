using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFoodOperator.Api.Data.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public required decimal PriceModefier { get; set; }
        public ICollection<ProductIngredient> ProductIngredients { get; } = [];
    }
}
