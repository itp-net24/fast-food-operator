using System.ComponentModel.DataAnnotations;

namespace FastFoodOperator.Api.Data.Models
{
	/// <summary>
	/// Links an order with a specific item and includes the quantity of the items ordered
	/// This class has composite key of OrderId and ItemId
	/// </summary>
	public class OrderItem
	{
		/// <summary>
		/// This is a foreign key to the <see cref="Order"/> class.
		/// </summary>
		public int OrderId { get; set; }
		
		/// <summary>
		/// This is a foreign key to the <see cref="Item"/> class.
		/// </summary>
		public int ItemId { get; set; }

		/// <summary>
		/// Sets the quantity of the items for the customers order
		/// </summary>
		[Required]
		public int Quantity { get; set; }

		/// <summary>
		/// Navigation property for <see cref="Order"/>.
		/// </summary>
		public Order? Order { get; set; }

		/// <summary>
		/// Navigation property for <see cref="Item"/>.
		/// </summary>
		//public Item Item { get; set; }
	}
}
