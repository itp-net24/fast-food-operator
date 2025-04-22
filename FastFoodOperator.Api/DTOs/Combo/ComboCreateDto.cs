using System.ComponentModel.DataAnnotations;

namespace FastFoodOperator.Api.DTOs.Combo;

public class ComboCreateDto
{
	[Required]
	[MinLength(3, ErrorMessage = "Combo name must be at least 3 characters long.")]	
	public required string Name { get; set; }
	
	[Required]
	[Range(0, int.MaxValue, ErrorMessage = "Base price must be a non-negative value.")]	
	public decimal BasePrice { get; set; }
	
	[Required(ErrorMessage = "At least one product must be included in the combo.")]	
	public ComboProductCreateDto[] Products { get; set; } = [];
}