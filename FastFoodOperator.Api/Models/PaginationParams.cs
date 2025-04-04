using System.ComponentModel.DataAnnotations;

namespace FastFoodOperator.Api.Models;

public class PaginationParams
{
	[Range(1, int.MaxValue, ErrorMessage = "Limit must be at least 1.")]
	public int Limit { get; set; } = 5;

	[Range(0, int.MaxValue, ErrorMessage = "Offset cannot be negative.")]
	public int Offset { get; set; } = 0;
}