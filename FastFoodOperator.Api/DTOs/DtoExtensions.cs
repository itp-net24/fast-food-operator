using FastFoodOperator.Api.DTOs.OrderDTOs;
using FastFoodOperator.Api.DTOs.OrderProductDTOs;
using FastFoodOperator.Api.DTOs.OrderComboDTOs;
using FastFoodOperator.Api.Entities;

namespace FastFoodOperator.Api.DTOs
{
	public static class DtoExtensions
	{
		public static GetOrderDto OrderToOrderDto(this Order order)
		{
			var orderDto = new GetOrderDto
			{
				OrderId = order.Id,
				OrderNumber = order.OrderNumber,
				CreatedAt = order.CreatedAt,
				CustomerNote = order.CustomerNote,
				OrderProducts = order.OrderProducts.Select(op => new OrderProductDto
				{
					ProductId = op.ProductId,
					ProductName = op.Product.Name,
					Quantity = op.Quantity
				}).ToList(),
				OrderCombos = order.OrderCombos.Select(oc => new OrderComboDto
				{
					ComboId = oc.ComboId,
					ComboName = oc.Combo.Name,
					Quantity = oc.Quantity
				}).ToList()
			};

			return orderDto;
		}
	}
}
