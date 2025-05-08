using FastFoodOperator.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOperator.Api.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class TagController(ProductService service, ILogger<TagController> logger) : ControllerBase
	{

		[HttpGet]
		public async Task<IActionResult> GetCategories()
		{
			try
			{
				var categories = await service.GetTagsAsync();
				return Ok(categories);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Failed to get categories");
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetTags(int id)
		{
			try
			{
				var category = await service.GetTagsByIdAsync(id);

				if (category == null)
				{
					logger.LogError($"Could not find categori with this id: {category} category is null");
					return NotFound();
				}
				return Ok(category);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not find category with this id");
				return BadRequest(ex.Message);
			}
		}


	}
}
