using FastFoodOperator.Api.DTOs;
using FastFoodOperator.Api.Interfaces;
using FastFoodOperator.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOperator.Api.Controllers
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
				_logger.LogInformation("Attempting to add an order...");
				await _orderService.AddOrder(orderDto);
				return Ok(new { message = "Order added successfully" });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
	}
}
