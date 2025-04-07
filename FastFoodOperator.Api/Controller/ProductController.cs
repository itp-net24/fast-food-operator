using FastFoodOperator.Api.DTOs.Product;
using FastFoodOperator.Api.Models;
using FastFoodOperator.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOperator.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class ProductController(ProductService service, ILogger<ProductController> logger) : ControllerBase
{
	[HttpGet("/{id}")]
	public async Task<IActionResult> GetProduct(int id)
	{
		try
		{
			var product = await service.GetProductByIdAsync(id);
			if (product is null)
			{
				logger.LogError($"Could not find a product with this id: {id} product is null");
				return NotFound();
			}

			return Ok(product);
			
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Could not find a product with this id");
			return BadRequest(ex.Message);
		}
	}

    [HttpGet]
	public async Task<IActionResult> GetProducts([FromQuery] PaginationParams pagination)
	{
		try
		{
			var products = await service.GetProductsAsync(pagination.Limit, pagination.Offset);
			return Ok(products);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to get products");
			return BadRequest(ex.Message);
		}
	}

	[HttpPost]
	public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto dto)
	{
		try
		{
			var createdProduct = await service.CreateProductAsync(dto);
			return CreatedAtAction(nameof(AddProduct), new { id = createdProduct.Id });
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to create product, {ProductName}", dto.Name);
			return BadRequest(ex.Message);
		}
	}

	[HttpPut]
	public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDto dto)
	{
		try
		{
			await service.UpdateProductAsync(dto);
			return NoContent();
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to update product, {ProductName}", dto.Name);
			return BadRequest(ex.Message);
		}
	}
	
	[HttpPut("/{id}")]
	public async Task<IActionResult> DeleteProduct(int id)
	{
		try
		{
			await service.DeleteProductAsync(id);
			return NoContent();
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to delete product");
			return BadRequest(ex.Message);
		}
	}
}