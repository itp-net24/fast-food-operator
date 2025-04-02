using FastFoodOperator.Api.Data;
using FastFoodOperator.Api.DTOs;
using FastFoodOperator.Api.Entities;

namespace FastFoodOperator.Api.Services;

public class ProductService(AppDbContext context, ILogger<ProductService> logger)
{
	public async Task CreateNewComboAsync(ComboCreateDto dto)
	{
		logger.LogInformation("Creating a new combo: {ComboName}", dto.Name);
		
		await using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			var combo = new Combo
			{
				Name = dto.Name,
				BasePrice = dto.BasePrice
			};

			context.Combos.Add(combo);

			var products = dto.Products.Select(p => new ComboProduct
			{
				ComboId = combo.Id,
				ProductId = p.ProductId,
				ProductVariantId = p.DefaultVariantId
			}).ToArray();

			context.ComboProducts.AddRange(products);

			await context.SaveChangesAsync();
			await transaction.CommitAsync();
			
			logger.LogInformation("Successfully created combo: {ComboId}", combo.Id);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to create combo: {ComboName}", dto.Name);	
			
			await transaction.RollbackAsync();
			throw;
		}
	}
}