namespace FastFoodOperator.Api.Entities
{
	/// <summary>
	/// Represents a customer's order, including order ID, number, status, and date of the order.
	/// The order may contain multiple items and combos, represented by the associated <see cref="OrderProduct"/> and <see cref="OrderCombo"/> lists.
	/// </summary>
	public class Order
	{
		public int Id { get; set; }
		public ICollection<OrderProduct> OrderProducts { get; set; } = [];
		public ICollection<OrderCombo> OrderCombos { get; set; } = [];
		public int OrderNumber { get; set; }
		public string CustomerNote { get; set; } = string.Empty;
		public OrderStatus OrderStatus { get; set; } = OrderStatus.Created;

		public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
		public DateTime StartedAt { get; set; }
		public DateTime CompletedAt { get; set; }

	}

	public enum OrderStatus
	{
		Created,
		InProgress,
		Completed
	}
}
