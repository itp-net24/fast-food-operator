using FastFoodOperator.Api.Data;
using FastFoodOperator.Api.DTOs;
using FastFoodOperator.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastFoodOperator.Api.Services;

public class ProductService(AppDbContext context, ILogger<ProductService> logger)
{
	#region Combo
	public async Task<ComboResponseDto[]> GetCombosAsync(int limit = 5, int offset = 0)
	{
		logger.LogInformation("Fetching combos with limit {Limit} and offset {Offset}", limit, offset);

		try
		{
			var combos = await context.Combos
				.AsNoTracking()
				.OrderBy(c => c.Name)
				.Skip(offset)
				.Take(limit)
				.Select(c => new ComboResponseDto
				{
					Id = c.Id,
					Name = c.Name,
					BasePrice = c.BasePrice,
				})
				.ToArrayAsync();
			
			logger.LogInformation("Fetched {Count} combos", combos.Length);

			return combos;
		}
		catch (Exception)
		{
			logger.LogError("Failed fetching combos with limit {Limit} and offset {Offset}", limit, offset);
			throw;
		}
	}

	public async Task<ComboResponseDto[]> GetCombosByProductIdAsync(int productId)
	{
		logger.LogInformation("Fetching combos for product {ProductId}", productId);

		try
		{
			var combos = await context.ComboProducts
				.AsNoTracking()
				.Where(c => c.ProductId == productId)
				.Include(cp => cp.Combo)
				.Select(cp => new ComboResponseDto
				{
					Id = cp.ComboId,
					Name = cp.Combo.Name,
					BasePrice = cp.Combo.BasePrice,
				})
				.ToArrayAsync();
			
			if (combos.Length == 0)
				logger.LogWarning("No combos found for product {ProductId}", productId);
			else
				logger.LogInformation("Fetched {Count} combos for product {ProductId}", combos.Length, productId);

			return combos;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error fetching combos for product {ProductId}", productId);
			throw;
		}
	}
	
	public async Task CreateComboAsync(ComboCreateDto dto)
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
				.FirstOrDefaultAsync(c => c.Id == dto.Id);

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

			logger.LogInformation("Successfully updated combo {ComboId}", dto.Id);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to update combo: {ComboId}", dto.Id);
			throw;
		}
	}

	public async Task DeleteComboAsync(int id)
	{
		logger.LogInformation("Deleting combo with ID: {ComboId}", id);
		var combo = new Combo { Id = id };

		try
		{
			context.Combos.Remove(combo);
			await context.SaveChangesAsync();
			logger.LogInformation("Successfully deleted combo: {ComboId}", id);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to delete combo: {ComboId}", id);
			throw;
		}
	}
	#endregion
	
	#region Product
	public async Task<ProductResponseDto[]> GetProductsAsync(int limit = 5, int offset = 0)
	{
		logger.LogInformation("Fetching products with limit {Limit} and offset {Offset}", limit, offset);

		try
		{
			var products = await context.Products
				.AsNoTracking()
				.OrderBy(p => p.CategoryId)
				.Skip(offset)
				.Take(limit)
				.Select(p => new ProductResponseDto
				{
					Id = p.Id,
					Name = p.Name,
					Description = p.Description,
					BasePrice = p.BasePrice
				})
				.ToArrayAsync();

			logger.LogInformation("Fetched {Count} products", products.Length);
			
			return products;
		}
		catch (Exception ex)
		{
			 logger.LogError(ex, "Error fetching products with limit {Limit} and offset {Offset}", limit, offset);
			 throw;
		}
	}

	public async Task<ProductResponseDto[]> GetProductsByCategoryIdAsync(int categoryId, int limit = 5, int offset = 0)
	{
		logger.LogInformation("Fetching products for category {CategoryId} with limit {Limit} and offset {Offset}", categoryId, limit, offset);
		
		try
		{
			var products = await context.Products
				.AsNoTracking()
				.Where(p => p.CategoryId == categoryId)
				.OrderBy(p => p.Id)
				.Skip(offset)
				.Take(limit)
				.Select(p => new ProductResponseDto
				{
					Id = p.Id,
					Name = p.Name,
					Description = p.Description,
					BasePrice = p.BasePrice
				})
				.ToArrayAsync();

			if (products.Length == 0)
				logger.LogWarning("No products found for category {CategoryId}", categoryId);
			else
				logger.LogInformation("Fetched {Count} products for category {CategoryId}", products.Length, categoryId);
			
			return products;
		}
		catch (Exception ex)
		{
			 logger.LogError(ex, "Error fetching products for category {CategoryId} with limit {Limit} and offset {Offset}", categoryId, limit, offset);
			 throw;
		}
	}

	public async Task CreateProductAsync(ProductCreateDto dto)
	{
		logger.LogInformation("Creating a new product: {ProductName}", dto.Name);
		
		await using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			var product = new Product
			{
				Name = dto.Name,
				Description = dto.Description,
				BasePrice = dto.BasePrice,
				CategoryId = dto.CategoryId,
			};

			context.Products.Add(product);
			await context.SaveChangesAsync();

			var ingredients = dto.Ingredients
				.Select(i => new ProductIngredient
				{
					ProductId = product.Id,
					IngredientId = i.IngredientId,
					Required = i.IsRequired
				});

			context.ProductIngredients.AddRange(ingredients);
			await context.SaveChangesAsync();

			await transaction.CommitAsync();
			
			logger.LogInformation("Successfully created product: {ProductId}", product.Id);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to create product: {ProductName}", dto.Name);	
			throw;
		}
	}

	public async Task UpdateProductAsync(ProductUpdateDto dto)
	{
		logger.LogInformation("Updating product: {ProductName}", dto.Name);

		try
		{
			var product = await context.Products
				.Include(c => c.ProductIngredients)
				.FirstOrDefaultAsync(p => p.Id == dto.Id);

			if (product is null)
			{
				logger.LogWarning("Product with ID {ProductId} not found", dto.Id);
				return;
			}

			product.Name = dto.Name ?? product.Name;
			product.Description = dto.Description ?? product.Description;
			product.BasePrice = dto.BasePrice ?? product.BasePrice;

			var existingIngredients = product.ProductIngredients.ToHashSet();
			var newIngredients = dto.Ingredients
				.Select(p => new ProductIngredient
				{
					ProductId = product.Id,
					IngredientId = p.IngredientId,
					Required = p.Required
				}).ToHashSet();

			var ingredientsToRemove = existingIngredients.Except(newIngredients);
			var ingredientsToAdd = newIngredients.Except(existingIngredients);

			context.ProductIngredients.RemoveRange(ingredientsToRemove);
			context.ProductIngredients.AddRange(ingredientsToAdd);

			await context.SaveChangesAsync();

			logger.LogInformation("Successfully updated product: {ProductId}", dto.Id);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to update product: {ProductId}", dto.Id);
			throw;
		}
	}

	public async Task DeleteProductAsync(int id)
	{
		logger.LogInformation("Deleting product: {ProductId}", id);
		var product = new Product { Id = id };
		
		try
		{
			context.Products.Remove(product);
			await context.SaveChangesAsync();
			
			logger.LogInformation("Successfully deleted product: {ProductId}", product.Id);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to delete product: {Id}", id);
			throw;
		}
	}
	#endregion
}