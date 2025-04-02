using System.ComponentModel.DataAnnotations;

namespace FastFoodOperator.Api.DTOs;

public class ComboProductCreateDto
{
	[Required]
	public int ProductId { get; set; }
	public int? DefaultVariantId { get; set; }
}