using FastFoodOperator.Api.DTOs.Ingredient;
using FastFoodOperator.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOperator.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class IngredientController(ProductService service) : ControllerBase
{
	/// <summary>
	/// Gets an ingredient by its ID.
	/// </summary>
	/// <param name="id">The ID of the ingredient to retrieve.</param>
	/// <returns>The ingredient if found, or an appropriate error response.</returns>
	/// <response code="200">If the ingredient was found</response>
	/// <response code="404">If the ingredient does not exist</response>
	/// <response code="400">If the request is invalid</response>
	/// <response code="500">If an unexpected error occurs</response>
	[HttpGet("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetIngredient(int id)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		try
		{
			var ingredient = await service.GetIngredientByIdAsync(id);
			return ingredient is null ? NotFound() : Ok(ingredient);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured");
		}
	}

	/// <summary>
	/// Gets a list of all ingredients.
	/// </summary>
	/// <returns>A list of ingredients or an appropriate error response.</returns>
	/// <response code="200">Returns the list of ingredients</response>
	/// <response code="400">If the request is invalid</response>
	/// <response code="500">If an unexpected error occurs</response>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetIngredients()
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		try
		{
			var ingredients = await service.GetIngredientsAsync(1000);
			return Ok(ingredients);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured");
		}
	}

	/// <summary>
	/// Adds a new ingredient.
	/// </summary>
	/// <param name="dto">The ingredient to add.</param>
	/// <returns>The created ingredient or an appropriate error response.</returns>
	/// <response code="201">If the ingredient was successfully created</response>
	/// <response code="400">If the request is invalid</response>
	/// <response code="500">If an unexpected error occurs</response>
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> AddIngredient([FromBody] IngredientCreateDto dto)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		try
		{
			var createdIngredient = await service.CreateIngredientAsync(dto);
			return CreatedAtAction(nameof(GetIngredient), new { id = createdIngredient.Id }, createdIngredient);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured");
		}
	}

	/// <summary>
	/// Updates an existing ingredient.
	/// </summary>
	/// <param name="dto">The updated ingredient data.</param>
	/// <returns>No content on success, or an appropriate error response.</returns>
	/// <response code="204">If the ingredient was successfully updated</response>
	/// <response code="400">If the request is invalid</response>
	/// <response code="500">If an unexpected error occurs</response>
	[HttpPut]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateIngredient([FromBody] IngredientUpdateDto dto)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		try
		{
			await service.UpdateIngredientAsync(dto);
			return NoContent();
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured");
		}
	}

	/// <summary>
	/// Deletes an ingredient by its ID.
	/// </summary>
	/// <param name="id">The ID of the ingredient to delete.</param>
	/// <returns>No content on success, or an appropriate error response.</returns>
	/// <response code="204">If the ingredient was successfully deleted</response>
	/// <response code="400">If the request is invalid</response>
	/// <response code="500">If an unexpected error occurs</response>
	[HttpDelete("{id:int}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> DeleteIngredient(int id)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		try
		{
			await service.DeleteIngredientByIdAsync(id);
			return NoContent();
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured");
		}
	}
}