using FastFoodOperator.Api.DTOs.Product;
using FastFoodOperator.Api.DTOs.ProductVariant;
using FastFoodOperator.Api.DTOs.Tags;
using FastFoodOperator.Api.Entities;

namespace FastFoodOperator.Api.DTOs.Combo;

public class ComboResponseDto
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public decimal BasePrice { get; set; }
	public string? ImageUrl { get; set; }
	
	public int? MainComboProductId { get; set; }
	
	public ComboProductResponseDto[] ComboProducts { get; set; } = [];
	public ComboGroupResponseDto[] ComboGroups { get; set; } = [];
	public TagResponseDto[] Tags { get; set; } = [];
}

public class ComboGroupResponseDto
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	
	public int? DefaultComboProductId { get; set; }
	
	public ComboProductResponseDto[] ComboProducts { get; set; } = [];
}

public class ComboProductResponseDto
{
	public int Id { get; set; }
	public int? DefaultProductVariantId { get; set; }
	
	public ProductResponseDto? Product { get; set; }
}