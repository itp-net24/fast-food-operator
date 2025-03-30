using System.ComponentModel.DataAnnotations;

namespace FastFoodOperator.Api.Data.Models
{
	/// <summary>
	/// Represents a customer's order, including order ID, number, status, and date of the order.
	/// The order may contain multiple items and combos, represented by the associated <see cref="OrderItem"/> and <see cref="OrderCombo"/> lists.
	/// </summary>
	public class Order
	{
		[Key]
		public int OrderId { get; set; }
	
		/// <summary>
		/// For customer and kitchen reference
		/// </summary>
		[Required]
		public int OrderNumber { get; set; }
		
		/// <summary>
		/// Indicates wheter the order is completed or pending.
		/// </summary>
		[Required]
		public bool OrderStatus { get; set; }
		
		/// <summary>
		/// Gets or sets the date the order was placed.
		/// </summary>
		[Required]
		public DateTime Date { get; set; }

		/// <summary>
		/// List of order items associated with this order
		/// Each item represents an individual product in the order
		/// </summary>
		public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
		
		/// <summary>
		/// List of order combos associated with this order
		/// Multiple items bundled together (e.g., Happy meal, Big Mac)
		/// </summary>
		public List<OrderCombo> OrderCombos { get; set; } = new List<OrderCombo>();
	}
}
