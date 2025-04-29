namespace FastFoodOperator.Api.Entities;

public class ComboGroup
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	
	public ICollection<Combo> Combos { get; set; } = [];

	public int? DefaultComboProductId { get; set; }
	public ComboProduct? DefaultComboProduct { get; set; }
	
	public ICollection<ComboProduct> ComboProducts { get; set; } = [];
}