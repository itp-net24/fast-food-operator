namespace FastFoodOperator.Api.Entities
{
	/// <summary>
	/// Links an order with a specific item and includes the quantity of the items ordered
	/// This class has composite key of OrderId and ItemId
	/// </summary>
	public class OrderProduct
	{
		/// <summary>
		/// This is a foreign key to the <see cref="Order"/> class.
		/// </summary>
		public int OrderId { get; set; }
		
		/// <summary>
		/// This is a foreign key to the <see cref="Product"/> class.
		/// </summary>
		public int ProductId { get; set; }

		
		/// <summary>
		/// Navigation property for <see cref="Order"/>.
		/// </summary>
		public Order Order { get; set; } = null!;

		/// <summary>
		/// Navigation property for <see cref="Product"/>.
		/// </summary>

		public Product Product { get; set; } = null!;
		
		
		/// <summary>
		/// Sets the quantity of the items for the customers order
		/// </summary>
		public int Quantity { get; set; }
	}
}
