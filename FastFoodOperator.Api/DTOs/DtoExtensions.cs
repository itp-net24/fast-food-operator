using FastFoodOperator.Api.Entities;

namespace FastFoodOperator.Api.DTOs
{
	public static class DtoExtensions
	{
		public static GetOrderDto OrderToOrderDto(this Order order)
		{
			var orderDto = new GetOrderDto
			{
				OrderNumber = order.OrderNumber,
				CreatedAt = order.CreatedAt,
				OrderProducts = order.OrderProducts.Select(op => new OrderProductDto
				{
					Quantity = op.Quantity,
				}).ToList(),
				OrderCombos = order.OrderCombos.Select(oc => new OrderComboDto
				{
					Quantity = oc.Quantity,
				}).ToList()
			};

			return orderDto;
		}
	}
}
