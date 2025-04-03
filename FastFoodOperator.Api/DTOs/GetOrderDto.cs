

namespace FastFoodOperator.Api.DTOs
{
	public class GetOrderDto
	{
		public int OrderNumber { get; set; }
		public DateTime CreatedAt { get; set; }
		public ICollection<OrderProductDto> OrderProducts { get; set; } = [];
		public ICollection<OrderComboDto> OrderCombos { get; set; } = [];
	}
}
