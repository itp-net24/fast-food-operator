using FastFoodOperator.Api.Data;
using FastFoodOperator.Api.DTOs;
using FastFoodOperator.Api.Entities;
using Microsoft.EntityFrameworkCore;

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

	public async Task UpdateComboAsync(ComboUpdateDto dto)
	{
		logger.LogInformation("Updating combo with ID: {ComboId}", dto.Id);

		try
		{
			var combo = await context.Combos
				.Include(c => c.ComboProducts)
				.FirstOrDefaultAsync();

			if (combo is null)
			{
				logger.LogWarning("Combo with ID {ComboId} not found", dto.Id);
				return;
			}

			combo.Name = dto.Name ?? combo.Name;
			combo.BasePrice = dto.BasePrice ?? combo.BasePrice;

			var existingProducts = combo.ComboProducts.ToHashSet();
			var newProducts = dto.Products
				.Select(p => new ComboProduct
				{
					ComboId = combo.Id,
					ProductId = p.ProductId,
					ProductVariantId = p.VariantId
				}).ToHashSet();

			var productsToRemove = existingProducts.Except(newProducts);
			var productsToAdd = newProducts.Except(existingProducts);

			context.ComboProducts.RemoveRange(productsToRemove);
			context.ComboProducts.AddRange(productsToAdd);

			await context.SaveChangesAsync();

			logger.LogInformation("Successfully updated combo {ComboId}", combo.Id);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to update combo: {ComboId}", dto.Id);
			throw;
		}
	}
}