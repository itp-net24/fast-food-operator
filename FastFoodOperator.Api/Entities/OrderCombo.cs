namespace FastFoodOperator.Api.Entities
{
	/// <summary>
	/// Links an order with a specific combo and includes the quantity of the combo ordered.
	/// This class has composite key of OrderId and ComboId
	/// </summary>
	public class OrderCombo
	{
		/// <summary>
		/// This is a foreign key to the <see cref="Order"/> class.
		/// </summary>
		public int OrderId { get; set; }

		/// <summary>
		/// This is a foreign key to the <see cref="Combo"/> class.
		/// </summary>
		public int ComboId { get; set; }

		
		/// <summary>
		/// Navigation property for <see cref="Order"/>.
		/// </summary>
		public Order Order { get; set; } = null!;

		/// <summary>
		/// Navigation property for <see cref="Combo"/>.
		/// </summary>
		public Combo Combo { get; set; } = null!;
		

		/// <summary>
		/// Sets the quantity of Combos for the customers order.
		/// </summary>
		public int Quantity { get; set; }
	}
}
