using FastFoodOperator.Api.DTOs.Combo;
using FastFoodOperator.Api.Services;
<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Http.HttpResults;
>>>>>>> develop
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOperator.Api.Controller
{
    [Route("api/[controller]")]
<<<<<<< HEAD
    [ApiController]
    public class ComboController(ProductService service, ILogger<ComboController> logger) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCombo(int id)
        {
            try
            {
                var combo = await service.GetComboByIdAsync(id);
=======
[ApiController]
    public class ComboController(ProductService service, ILogger<ComboController> logger) : ControllerBase
{
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCombo(int id)
	{
            try
            {
		var combo = await service.GetComboByIdAsync(id);
>>>>>>> develop
                if (combo == null)
                {
                    logger.LogError($"Could not find combo with this id: {combo} combo is null");
                    return NotFound();
                }

<<<<<<< HEAD
                return Ok(combo);
            }
=======
		return Ok(combo);
	}
>>>>>>> develop
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

<<<<<<< HEAD
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
=======
        //[HttpPut]
        //public async Task<IActionResult> UpdateCombo([FromBody] ComboUpdateDto dto)
        //{
        //    try
        //    {
        //        await service.UpdateComboAsync(dto);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex, "Failed to update combo");
        //        return BadRequest(ex.Message);
        //    }
        //}

    }
}
>>>>>>> develop
