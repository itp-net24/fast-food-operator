using FastFoodOperator.Api.DTOs.Orders;
using FastFoodOperator.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FastFoodOperator.Api.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;
		private readonly ILogger<OrderController> _logger;

		public OrderController(IOrderService orderService, ILogger<OrderController> logger)
		{
			_orderService = orderService;
			_logger = logger;
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddOrder([FromBody] AddOrderDto orderDto)
		{
			try
			{
				var response = await _orderService.AddOrder(orderDto);
				return Ok(new { message = $"{response}" });
			}
			catch (Exception ex)
			{
				return BadRequest(new { Ordernumber = ex.Message });
			}
		}

		[HttpPut("completeOrder")]
		public async Task<IActionResult> CompleteOrder([FromBody] UpdateOrderDto order)
		{
			try
			{
				await _orderService.CompleteOrder(order);
				return Ok(new { message = "Order completed successfully." });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpGet("displayOrderNumbers")]
		public async Task<IActionResult> GetOrderNumbers()
		{
			var result = await _orderService.DisplayOrderNumbers();
			return Ok(result);
		}

		[HttpGet("GetOrders")]
		public async Task<ActionResult<List<GetOrderDto>>> GetOrders()
		{
			var result = await _orderService.GetOrders();
			return Ok(result);
		}

		[HttpPut("startOrder")]
		public async Task<IActionResult> StartOrder([FromBody] UpdateOrderDto order)
		{
			try
			{
				await _orderService.StartOrder(order);
				return Ok(new { message = "Order updated successfully" });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteOrder(int id)
		{
			try
			{
				await _orderService.DeleteOrder(id);
				return NoContent();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to delete product");
				return BadRequest(ex.Message);
			}
		}
	}
}
