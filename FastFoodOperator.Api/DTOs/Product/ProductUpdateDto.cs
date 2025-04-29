<<<<<<< HEAD
=======
using System.ComponentModel.DataAnnotations;

>>>>>>> develop
namespace FastFoodOperator.Api.DTOs.Product;

public class ProductUpdateDto
{
	public int Id { get; init; }
<<<<<<< HEAD
	public string? Name { get; init; }
	public string? Description { get; init; }
	public decimal? BasePrice { get; init; }
=======
	
	[Required]
	[MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]	
	public string? Name { get; init; }
	public string? Description { get; init; }
	
	[Required]
	[Range(0, int.MaxValue, ErrorMessage = "Base price must be a non-negative value.")]	
	public decimal? BasePrice { get; init; }
	
	[Required]
	[Range(0, int.MaxValue, ErrorMessage = "Category id must be a non-negative integer.")]
	public int CategoryId { get; init; }
	
>>>>>>> develop
	public ProductIngredientUpdateDto[] Ingredients { get; init; } = [];
	public string? PictureUrl { get; init; }
}