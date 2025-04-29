<<<<<<< HEAD
=======
using System.ComponentModel.DataAnnotations;

>>>>>>> develop
namespace FastFoodOperator.Api.DTOs.Product;

public class ProductIngredientUpdateDto
{
<<<<<<< HEAD
=======
	[Required]
	[Range(0, int.MaxValue, ErrorMessage = "Ingredient id must be a non-negative integer.")]
>>>>>>> develop
	public int IngredientId { get; init; }
	public bool Required { get; init; }
}