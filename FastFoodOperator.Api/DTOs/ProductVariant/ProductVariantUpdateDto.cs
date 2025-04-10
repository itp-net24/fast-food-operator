using System.ComponentModel.DataAnnotations;

namespace FastFoodOperator.Api.DTOs.ProductVariant;

public class ProductVariantUpdateDto
{
	[Required]
	[Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative integer.")]
	public int Id { get; set; }
	
	[Required]
	[Range(0, int.MaxValue, ErrorMessage = "Product id must be a non-negative integer.")]
	public int ProductId { get; set; }
	
	[Required]
	[MinLength(3, ErrorMessage = "Variant name must be at least 3 characters long.")]	
	public string? Name { get; set; } = string.Empty;
	
	public string? Description { get; set; }
	
	[Required]
	[Range(0, int.MaxValue, ErrorMessage = "Price modifier must be a non-negative value.")]	
	public decimal? PriceModifier { get; set; }
}