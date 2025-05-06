using FastFoodOperator.Api.DTOs.Combo;
using FastFoodOperator.Api.DTOs.Product;

namespace FastFoodOperator.Api.DTOs.Orders
{
	public class OrderReceiptDto
	{
		public List<ProductReceiptDto> Products { get; set; } = [];
		public List<ComboReceiptDto> Combos { get; set; } = [];
		public DateTime CreatedAt { get; set; }
		public int OrderNumber { get; set; }
		public decimal TotalVatSixPercent { get; set; }
		public decimal TotalVatTwelvePercent { get; set; }
		public decimal TotalVatTwentyFivePercent { get; set; }
		public decimal TotalTax { get; set; }
		public decimal OrderFinalPrice { get; set; }
		public string CustomerNote = string.Empty!;
	}
}
