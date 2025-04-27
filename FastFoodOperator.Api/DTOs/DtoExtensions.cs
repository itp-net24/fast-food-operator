using FastFoodOperator.Api.DTOs.Orders;
using FastFoodOperator.Api.DTOs.OrderProduct;
using FastFoodOperator.Api.DTOs.OrderCombo;
using FastFoodOperator.Api.DTOs.Ingredient;
using FastFoodOperator.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;


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
				OrderStatus = order.OrderStatus,
				OrderProducts = order.OrderProducts.Select(op => new OrderProductDto
				{
					ProductName = op.ProductName, 
					Quantity = op.Quantity,
					Ingredients = op.ProductIngredients
				}).ToList(),
				OrderCombos = order.OrderCombos.Select(oc => new OrderComboDto
				{
					ComboName = oc.ComboName,  
					Quantity = oc.Quantity,
				}).ToList()
			};

			return orderDto;
		}

	}


}
