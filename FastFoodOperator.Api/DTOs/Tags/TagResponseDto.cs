namespace FastFoodOperator.Api.DTOs.Tags;

public class TagResponseDto
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public decimal TaxRate { get; set; }
}
