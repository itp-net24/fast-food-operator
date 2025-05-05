using FastFoodOperator.Api.DTOs.Product;
using FastFoodOperator.Api.Models;
using FastFoodOperator.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOperator.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class ProductController(ProductService service) : ControllerBase
{
	/// <summary>
	/// Gets a product by its ID.
	/// </summary>
	/// <param name="id">The product ID.</param>
	/// <returns>The product if found; otherwise, an appropriate error.</returns>
	/// <response code="200">Returns the list of products (can be empty)</response>
	/// <response code="400">If the pagination parameters are invalid</response>
	/// <response code="500">If an unexpected error occurs</response>
	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetProduct(int id)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);
		
		try
		{
			var product = await service.GetProductByIdAsync(id);
			return product is null ? NotFound() : Ok(product);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured");
		}
	}

	/// <summary>
	/// Retrieves a paginated list of products.
	/// </summary>
	/// <param name="pagination">Pagination parameters (limit and offset).</param>
	/// <returns>A list of products, or a 500 status code on error.</returns>
	/// <response code="200">Returns the list of products (can be empty)</response>
	/// <response code="400">If the pagination parameters are invalid</response>
	/// <response code="500">If an unexpected error occurs</response>
	[HttpGet]
	[ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
	public async Task<IActionResult> GetProducts([FromQuery] PaginationParams pagination)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);
		
		try
		{
			var products = await service.GetProductsAsync(pagination.Limit, pagination.Offset);
			return Ok(products);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured");
		}
	}

	[HttpGet("GetProductsByCategory")]
    public async Task<IActionResult> GetProductsByCategory(int id)
    {
        try
        {
            var products = await service.GetProductsByCategoryIdAsync(id);
            return Ok(products);
        } 
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
	
	/// <summary>
	/// Creates a new product.
	/// </summary>
	/// <param name="dto">The product data to create.</param>
	/// <returns>The newly created product with location header.</returns>
	/// <response code="201">Returns the newly created product</response>
	/// <response code="400">If the product data is invalid</response>
	/// <response code="500">If an unexpected error occurs</response>
	[HttpPost]
	[ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	//public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto dto)
	//{
	//	if (!ModelState.IsValid)
	//		return BadRequest(ModelState);
		
	//	try
	//	{
	//		var createdProduct = await service.CreateProductAsync(dto);
	//		return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
	//	}
	//	catch (Exception)
	//	{
	//		return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured");
	//	}
	//}

	/// <summary>
	/// Updates an existing product.
	/// </summary>
	/// <param name="dto">The updated product information.</param>
	/// <returns>No content on success, or an appropriate error response.</returns>
	/// <response code="204">If the product was successfully updated</response>
	/// <response code="400">If the input data is invalid</response>
	/// <response code="500">If an unexpected error occurs</response>
	[HttpPut]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDto dto)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);
		
		try
		{
			await service.UpdateProductAsync(dto);
			return NoContent();
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured");
		}
	}
	
	/// <summary>
	/// Deletes a product by its ID.
	/// </summary>
	/// <param name="id">The ID of the product to delete.</param>
	/// <returns>No content on success, or an appropriate error response.</returns>
	/// <response code="204">If the product was successfully deleted</response>
	/// <response code="400">If the request is invalid</response>
	/// <response code="500">If an unexpected error occurs</response>
	[HttpDelete("{id:int}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> DeleteProduct(int id)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);
		
		try
		{
			await service.DeleteProductAsync(id);
			return NoContent();
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured");
		}
	}
}
