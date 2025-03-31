using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFoodOperator.Api.Data
{
    public class Combo
    {
        [Key]
        public int Id { get; set; }

        public ICollection<ComboProduct> ComboProducts { get; set; } = [];


        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal BasePrice { get; set; }
	}   
}
