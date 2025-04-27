using FastFoodOperator.Api.DTOs.Combo;
using FastFoodOperator.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOperator.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboController(ProductService service, ILogger<ComboController> logger) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCombo(int id)
        {
            try
            {
                var combo = await service.GetComboByIdAsync(id);
                if (combo == null)
                {
                    logger.LogError($"Could not find combo with this id: {combo} combo is null");
                    return NotFound();
                }

                return Ok(combo);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not find a combo with this id");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCombos()
        {
            try
            {
                var combos = await service.GetCombosAsync();
                return Ok(combos);
            }
            catch(Exception ex)
            {
                logger.LogError("Failed to get combos");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCombo([FromBody] ComboUpdateDto dto)
        {
            try
            {
                await service.UpdateComboAsync(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to update combo");
                return BadRequest(ex.Message);
            }
        }

    }
}
