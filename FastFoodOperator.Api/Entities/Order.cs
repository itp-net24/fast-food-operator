namespace FastFoodOperator.Api.Entities
{
	/// <summary>
	/// Represents a customer's order, including order ID, number, status, and date of the order.
	/// The order may contain multiple items and combos, represented by the associated <see cref="OrderProduct"/> and <see cref="OrderCombo"/> lists.
	/// </summary>
	public class Order
	{
		public int Id { get; set; }
	
		/// <summary>
		/// List of order items associated with this order
		/// Each item represents an individual product in the order
		/// </summary>
		public ICollection<OrderProduct> OrderProducts { get; set; } = [];

		/// <summary>
		/// List of order combos associated with this order
		/// Multiple items bundled together (e.g., Happy meal, Big Mac)
		/// </summary>
		public ICollection<OrderCombo> OrderCombos { get; set; } = [];
		
		
		/// <summary>
		/// For customer and kitchen reference
		/// </summary>
		public int OrderNumber { get; set; }
		
		/// <summary>
		/// Indicates wheter the order is completed or pending.
		/// </summary>
		public bool OrderStatus { get; set; }
		
		/// <summary>
		/// Gets the date the order was placed.
		/// </summary>
		public DateTime CreatedAt { get; set; }

	}
}
