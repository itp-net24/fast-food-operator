using FastFoodOperator.Api.Models;
using FastFoodOperator.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOperator.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class ProductController(ProductService service, ILogger<ProductController> logger) : ControllerBase
{
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
}