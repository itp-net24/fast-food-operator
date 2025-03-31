using System.ComponentModel.DataAnnotations.Schema;

namespace FastFoodOperator.Api.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public required decimal BasePrice { get; set; }
        public ICollection<ComboProduct> ComboProducts { get; set; } = [];
        public ICollection<ProductIngredient> ProductIngredients { get; } = [];
        public required Category ItemCategory { get; set; }
    }
}
