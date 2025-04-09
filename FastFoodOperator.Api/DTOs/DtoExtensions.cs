using FastFoodOperator.Api.DTOs.OrderDTOs;
using FastFoodOperator.Api.DTOs.OrderProductDTOs;
using FastFoodOperator.Api.DTOs.OrderComboDTOs;
using FastFoodOperator.Api.DTOs.IngredientsDto;
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
					Quantity = op.Quantity,
					Ingredients = op.Product.ProductIngredients.Select(pi => new IngredientDto
					{
						IngredientName = pi.Ingredient.Name
					}).ToList()

				}).ToList(),
				OrderCombos = order.OrderCombos.Select(oc => new OrderComboDto
				{
					ComboId = oc.ComboId,
					ComboName = oc.Combo.Name,
					Quantity = oc.Quantity,
					Ingredients = oc.Combo.ComboProducts
						.SelectMany(cp => cp.Product.ProductIngredients)
						.Select(pi => new IngredientDto
						{
							IngredientName = pi.Ingredient.Name
						}).ToList()
				}).ToList()
			};

			return orderDto;
		}

	}
}
