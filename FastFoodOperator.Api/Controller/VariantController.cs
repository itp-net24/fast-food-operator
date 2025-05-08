using FastFoodOperator.Api.DTOs.ProductVariant;
using FastFoodOperator.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOperator.Api.Controller
{
	[ApiController]
	[Route("api/[controller]")]
    public class VariantController(ProductService service, ILogger<VariantController> logger) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVariantByProductId(int id)
        {
            try
            {
                var variant = await service.GetVariantsByProductIdAsync(id);
                if (variant == null)
                {
                    logger.LogError($"Could not find variant for productId: {id}");
                    return NotFound();
                }

                return Ok(variant);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Could not find variants for product with this id");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVariant([FromBody] ProductVariantUpdateDto dto)
        {
            try
            {
                await service.UpdateProductVariantAsync(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to update variant");
                return BadRequest(ex.Message);
            }
        }        
    }
}
