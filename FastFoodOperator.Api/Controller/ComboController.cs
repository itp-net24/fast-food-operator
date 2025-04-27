using FastFoodOperator.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOperator.Api.Controller;

[ApiController]
[Route("[controller]")]
public class ComboController(ProductService service) : ControllerBase
{
	[HttpGet("{id:int}")]
	public async Task<IActionResult> Get(int id)
	{
		var combo = await service.GetComboByIdAsync(id);
		return Ok(combo);
	}
}