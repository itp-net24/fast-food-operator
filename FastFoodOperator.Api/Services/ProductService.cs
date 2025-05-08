using FastFoodOperator.Api.Data;
using FastFoodOperator.Api.DTOs.Combo;
using FastFoodOperator.Api.DTOs.Ingredient;
using FastFoodOperator.Api.DTOs.Product;
using FastFoodOperator.Api.DTOs.ProductVariant;
using FastFoodOperator.Api.DTOs.Tags;
using FastFoodOperator.Api.Entities;

using Microsoft.EntityFrameworkCore;

namespace FastFoodOperator.Api.Services;

public class ProductService (AppDbContext context, ILogger<ProductService> logger)
{

	// #region Combo
	//
	public async Task<ComboResponseDto?> GetComboByIdAsync(int id)
	{
		logger.LogInformation("Fetching products for combo {ComboId}", id);
		
		try
		{
			var combo = await context.Combos
				.AsNoTracking()
				.Where(c => c.Id == id)
				.Select(c => new ComboResponseDto
				{
					Id = c.Id,
					Name = c.Name,
					BasePrice = c.BasePrice,
					Tags = c.Tags.Select(t => new TagResponseDto
						{
							Id = t.Tag.Id,
							Name = t.Tag.Name,
							TaxRate = t.Tag.TaxRate
						}).ToArray(),
					ImageUrl = c.ImageUrl,
					MainComboProductId = c.MainComboProductId,
					ComboProducts = c.ComboProducts.Select(sp => new ComboProductResponseDto
					{
						Id = sp.Id,
						DefaultProductVariantId = sp.DefaultVariantId,
						Product = new ProductResponseDto
						{
							Id = sp.Product.Id,
							Name = sp.Product.Name,
							Description = sp.Product.Description,
							BasePrice = sp.Product.BasePrice,
							Tags = sp.Product.Tags.Select(t => new TagResponseDto
							{
								Id = t.Tag.Id,
								Name = t.Tag.Name,
								TaxRate = t.Tag.TaxRate
							}).ToArray(),
							ImageUrl = sp.Product.ImageUrl,
							Variants = sp.Product.Variants.Select(v => new ProductVariantResponseDto
							{
								Id = v.Id,
								Name = v.Name,
								PriceModifier = v.PriceModifier,
								ProductId = v.ProductId,
							}).ToArray(),
							Ingredients = sp.Product.ProductIngredients.Select(i => new IngredientResponseDto
							{
								Id = i.Ingredient.Id,
								Name = i.Ingredient.Name,
								PriceModifier = i.Ingredient.PriceModifier
							}).ToArray()
						}
					}).ToArray(),
					ComboGroups = c.ComboGroups.Select(g => new ComboGroupResponseDto
					{
						Id = g.Id,
						Name = g.Name,
						DefaultComboProductId = g.DefaultComboProductId,
						ComboProducts = g.ComboProducts.Select(cp => new ComboProductResponseDto
						{
							Id = cp.Id,
							DefaultProductVariantId = cp.DefaultVariantId,
							Product = new ProductResponseDto
							{
								Id = cp.ProductId,
								Name = cp.Product.Name,
								Description = cp.Product.Description,
								BasePrice = cp.Product.BasePrice,
								Tags = cp.Product.Tags.Select(t => new TagResponseDto
								{
									Id = t.Tag.Id,
									Name = t.Tag.Name,
									TaxRate = t.Tag.TaxRate
								}).ToArray(),
								ImageUrl = cp.Product.ImageUrl,
								Variants = cp.Product.Variants.Select(v => new ProductVariantResponseDto
								{
									Id = v.Id,
									Name = v.Name,
									PriceModifier = v.PriceModifier,
								}).ToArray()
							}
						}).ToArray()
					}).ToArray()
				})
				.FirstOrDefaultAsync();
	
			// if (products.Length == 0)
			// 	logger.LogWarning("No products found for combo {ComboId}", id);
			// else
			// 	logger.LogInformation("Fetched {Count} products for combo {ComboId}", products.Length, id);
			//
			return combo;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error fetching products for combo {ComboId}", id);
			throw;
		}
	}

	//public async Task UpdateComboAsync(ComboUpdateDto dto)
	//{
	//	logger.LogInformation("Updating combo with ID: {ComboId}", dto.Id);

	//	try
	//	{
	//		var combo = await context.Combos
	//			.Include(c => c.ComboProducts)
	//			.FirstOrDefaultAsync(c => c.Id == dto.Id);

	//		if (combo is null)
	//		{
	//			logger.LogWarning("Combo with ID {ComboId} not found", dto.Id);
	//			return;
	//		}

	//		combo.Name = dto.Name ?? combo.Name;
	//		combo.BasePrice = dto.BasePrice ?? combo.BasePrice;

	//		var existingProducts = combo.ComboProducts.ToHashSet();
	//		var newProducts = dto.Products
	//			.Select(p => new ComboProduct
	//			{
	//				ComboId = combo.Id,
	//				ProductId = p.ProductId,
	//				ProductVariantId = p.VariantId
	//			}).ToHashSet();

	//		var productsToRemove = existingProducts.Except(newProducts);
	//		var productsToAdd = newProducts.Except(existingProducts);

	//		context.ComboProducts.RemoveRange(productsToRemove);
	//		context.ComboProducts.AddRange(productsToAdd);

	//		await context.SaveChangesAsync();

	//		logger.LogInformation("Successfully updated combo {ComboId}", dto.Id);
	//	}
	//	catch (Exception ex)
	//	{
	//		logger.LogError(ex, "Error fetching products for combo {ComboId}", id);
	//		throw;
	//	}
	//}

	public async Task<ProductMinimalResponseDto[]> GetCombosAsync(int limit = 5, int offset = 0)
	{
		logger.LogInformation("Fetching combos with limit {Limit} and offset {Offset}", limit, offset);

		try
		{
			var combos = await context.Combos
				.AsNoTracking()
				.Include(c => c.Tags)
					.ThenInclude(ct => ct.Tag)
				.OrderBy(c => c.Tags
					.OrderBy(ct => ct.TagId)
					.Select(ct => (int?)ct.TagId)
					.FirstOrDefault())
				.Skip(offset)
				.Take(limit)
				.Select(c => new ProductMinimalResponseDto
				{
					Id = c.Id,
					Name = c.Name,
					Tags = c.Tags.Select(t => new TagResponseDto
					{
						Id = t.Tag.Id,
						Name = t.Tag.Name,
						TaxRate = t.Tag.TaxRate
					}).ToArray(),
					BasePrice = c.BasePrice,
					ImageUrl = c.ImageUrl
				})
				.ToArrayAsync();

			if (combos.Length == 0)
				logger.LogWarning("No combos found");
			else
				logger.LogInformation("Fetched {Count} combos", combos.Length);

			return combos;
		}
		catch (Exception)
		{
			logger.LogError("Failed fetching combos with limit {Limit} and offset {Offset}", limit, offset);
			throw;
		}
	}
	// public async Task<ComboResponseDto[]> GetCombosByProductIdAsync(int productId)
	// {
	// 	logger.LogInformation("Fetching combos for product {ProductId}", productId);
	//
	// 	try
	// 	{
	// 		var combos = await context.ComboProducts
	// 			.AsNoTracking()
	// 			.Where(c => c.ProductId == productId)
	// 			.Include(cp => cp.Combo)
	// 			.Select(cp => new ComboResponseDto
	// 			{
	// 				Id = cp.ComboId,
	// 				Name = cp.Combo.Name,
	// 				BasePrice = cp.Combo.BasePrice,
	// 			})
	// 			.ToArrayAsync();
	// 		
	// 		if (combos.Length == 0)
	// 			logger.LogWarning("No combos found for product {ProductId}", productId);
	// 		else
	// 			logger.LogInformation("Fetched {Count} combos for product {ProductId}", combos.Length, productId);
	//
	// 		return combos;
	// 	}
	// 	catch (Exception ex)
	// 	{
	// 		logger.LogError(ex, "Error fetching combos for product {ProductId}", productId);
	// 		throw;
	// 	}
	// }
	//
	// public async Task CreateComboAsync(ComboCreateDto dto)
	// {
	// 	logger.LogInformation("Creating a new combo: {ComboName}", dto.Name);
	// 	
	// 	await using var transaction = await context.Database.BeginTransactionAsync();
	// 	try
	// 	{
	// 		var combo = new Combo
	// 		{
	// 			Name = dto.Name,
	// 			BasePrice = dto.BasePrice
	// 		};
	//
	// 		context.Combos.Add(combo);
	//
	// 		var products = dto.Products.Select(p => new ComboProduct
	// 		{
	// 			ComboId = combo.Id,
	// 			ProductId = p.ProductId,
	// 			ProductVariantId = p.DefaultVariantId
	// 		}).ToArray();
	//
	// 		context.ComboProducts.AddRange(products);
	//
	// 		await context.SaveChangesAsync();
	// 		await transaction.CommitAsync();
	// 		
	// 		logger.LogInformation("Successfully created combo: {ComboId}", combo.Id);
	// 	}
	// 	catch (Exception ex)
	// 	{
	// 		logger.LogError(ex, "Failed to create combo: {ComboName}", dto.Name);	
	// 		
	// 		await transaction.RollbackAsync();
	// 		throw;
	// 	}
	// }
	//
	// public async Task UpdateComboAsync(ComboUpdateDto dto)
	// {
	// 	logger.LogInformation("Updating combo with ID: {ComboId}", dto.Id);
	//
	// 	try
	// 	{
	// 		var combo = await context.Combos
	// 			.Include(c => c.ComboProducts)
	// 			.FirstOrDefaultAsync(c => c.Id == dto.Id);
	//
	// 		if (combo is null)
	// 		{
	// 			logger.LogWarning("Combo with ID {ComboId} not found", dto.Id);
	// 			return;
	// 		}
	//
	// 		combo.Name = dto.Name ?? combo.Name;
	// 		combo.BasePrice = dto.BasePrice ?? combo.BasePrice;
	//
	// 		var existingProducts = combo.ComboProducts.ToHashSet();
	// 		var newProducts = dto.Products
	// 			.Select(p => new ComboProduct
	// 			{
	// 				ComboId = combo.Id,
	// 				ProductId = p.ProductId,
	// 				ProductVariantId = p.VariantId
	// 			}).ToHashSet();
	//
	// 		var productsToRemove = existingProducts.Except(newProducts);
	// 		var productsToAdd = newProducts.Except(existingProducts);
	//
	// 		context.ComboProducts.RemoveRange(productsToRemove);
	// 		context.ComboProducts.AddRange(productsToAdd);
	//
	// 		await context.SaveChangesAsync();
	//
	// 		logger.LogInformation("Successfully updated combo {ComboId}", dto.Id);
	// 	}
	// 	catch (Exception ex)
	// 	{
	// 		logger.LogError(ex, "Failed to update combo: {ComboId}", dto.Id);
	// 		throw;
	// 	}
	// }
	//
	// public async Task DeleteComboAsync(int id)
	// {
	// 	logger.LogInformation("Deleting combo with ID: {ComboId}", id);
	// 	var combo = new Combo { Id = id };
	//
	// 	try
	// 	{
	// 		context.Combos.Remove(combo);
	// 		await context.SaveChangesAsync();
	// 		logger.LogInformation("Successfully deleted combo: {ComboId}", id);
	// 	}
	// 	catch (Exception ex)
	// 	{
	// 		logger.LogError(ex, "Failed to delete combo: {ComboId}", id);
	// 		throw;
	// 	}
	// }
	// #endregion


	#region Product

	public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
	{
		logger.LogInformation("Fetching product with ID: {ProductId}", id);

		try
		{
			var product = await context.Products
				.AsNoTracking()
				.Where(p => p.Id == id)
				.Include(p => p.ProductIngredients)
				.ThenInclude(pi => pi.Ingredient)
				.Select(p => new ProductResponseDto
				{
					Id = p.Id,
					Name = p.Name,
					Description = p.Description,
					BasePrice = p.BasePrice,
					ImageUrl = p.ImageUrl,
					Tags = p.Tags.Select(t => new TagResponseDto
					{
						Id = t.Tag.Id,
						Name = t.Tag.Name,
						TaxRate = t.Tag.TaxRate
					}).ToArray(),
					Variants = p.Variants.Select(v => new ProductVariantResponseDto
					{
						Id = v.Id,
						Name = v.Name,
						PriceModifier = v.PriceModifier,
						ProductId = v.ProductId,
					}).ToArray(),
					Ingredients = p.ProductIngredients.Select(pi => new IngredientResponseDto
					{
						Id = pi.IngredientId,
						Name = pi.Ingredient.Name,
						PriceModifier = pi.Ingredient.PriceModifier
					}).ToArray()
				})
				.FirstOrDefaultAsync();


			if (product == null)
			{
				logger.LogError("Product with ID: {ProductID} was not found", id);
				return null;
			}

			logger.LogInformation("Product with ID: {ProductId} has been found", id);
			return product;


		}	
		catch (Exception ex)
		{

			logger.LogError(ex, "Error fetching product with ID: {ProductId}", id);

			throw;
		}
	}

	public async Task<ProductMinimalResponseDto[]> GetProductsAsync(int limit = 5, int offset = 0)
	{
		logger.LogInformation("Fetching products with limit {Limit} and offset {Offset}", limit, offset);

		try
		{
			var products = await context.Products
				.AsNoTracking()
				.Include(p => p.Tags)
					.ThenInclude(pt => pt.Tag)
				.OrderBy(p => p.Tags
					.OrderBy(pt => pt.TagId)
					.Select(pt => (int?)pt.TagId)
					.FirstOrDefault())
				.Skip(offset)
				.Take(limit)
				.Select(p => new ProductMinimalResponseDto
				{
					Id = p.Id,
					Name = p.Name,
					Tags = p.Tags.Select(t => new TagResponseDto
					{
						Id = t.Tag.Id,
						Name = t.Tag.Name,
						TaxRate = t.Tag.TaxRate
					}).ToArray(),
					BasePrice = p.BasePrice,
					ImageUrl = p.ImageUrl
				})
				.ToArrayAsync();
			
			if (products.Length == 0)
				logger.LogWarning("No products found");
			else
				logger.LogInformation("Fetched {Count} products", products.Length);
			
			return products;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error fetching products with limit {Limit} and offset {Offset}", limit, offset);
			throw;
		}
	}

	public async Task<ProductMinimalResponseDto[]> GetProductsByCategoryIdAsync(int tagId)
	{
		logger.LogInformation("Fetching products for category {CategoryId}", tagId);
		
		try
		{
			var products = await context.Products
				.AsNoTracking()
				.Where(p => p.Tags.Any(t => t.TagId == tagId))
				.OrderBy(p => p.Id)
				.Select(p => new ProductMinimalResponseDto
				{
					Id = p.Id,
					Name = p.Name,
					Tags = p.Tags.Select(t => new TagResponseDto
					{
						Id = t.Tag.Id,
						Name = t.Tag.Name,
						TaxRate = t.Tag.TaxRate
					}).ToArray(),
					BasePrice = p.BasePrice,
					ImageUrl = p.ImageUrl
				})
				.ToArrayAsync();

			if (products.Length == 0)
				logger.LogWarning("No products found for category {CategoryId}", tagId);
			else
				logger.LogInformation("Fetched {Count} products for category {CategoryId}", products.Length, tagId);

			var combos = await context.Combos
				.AsNoTracking()
				.Where(c => c.Tags.Any(t => t.TagId == tagId))
				.OrderBy(c => c.Id)
				.Select(c => new ProductMinimalResponseDto
				{
					Id = c.Id,
					Name = c.Name,
					Tags = c.Tags.Select(t => new TagResponseDto
					{
						Id = t.Tag.Id,
						Name = t.Tag.Name,
						TaxRate = t.Tag.TaxRate
					}).ToArray(),
					BasePrice = c.BasePrice,
					ImageUrl = c.ImageUrl
				})
				.ToArrayAsync();

			return products.Concat(combos).ToArray();
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error fetching products for category {CategoryId}", tagId);
			throw;
		}
	}

	//public async Task<ProductResponseDto> CreateProductAsync(ProductCreateDto dto)
	//{
	//	logger.LogInformation("Creating a new product: {ProductName}", dto.Name);
		
	//	await using var transaction = await context.Database.BeginTransactionAsync();
	//	try
	//	{
	//		var product = new Product
	//		{
	//			Name = dto.Name,
	//			Description = dto.Description,
	//			BasePrice = dto.BasePrice,
	//			CategoryId = dto.CategoryId,
	//			ImageUrl = dto.PictureUrl
	//		};

	//		context.Products.Add(product);
	//		await context.SaveChangesAsync();

	//		var ingredients = dto.Ingredients
	//			.Select(i => new ProductIngredient
	//			{
	//				ProductId = product.Id,
	//				IngredientId = i.IngredientId,
	//				Required = i.IsRequired
	//			});

	//		context.ProductIngredients.AddRange(ingredients);
	//		await context.SaveChangesAsync();

	//		await transaction.CommitAsync();
			
	//		logger.LogInformation("Successfully created product: {ProductId}", product.Id);

	//		return new ProductResponseDto
	//		{
	//			Id = product.Id,
	//			Name = product.Name,
	//			Description = product.Description,
	//			BasePrice = product.BasePrice,
	//			ImageUrl = product.ImageUrl

	//		};
	//	}
	//	catch (Exception ex)
	//	{
	//		logger.LogError(ex, "Failed to create product: {ProductName}", dto.Name);
	//		await transaction.RollbackAsync();
	//		throw;
	//	}
	//}

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
			product.ImageUrl = dto.PictureUrl ?? product.ImageUrl;

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

	#region Variant

	public async Task<ProductVariantResponseDto[]> GetVariantsByProductIdAsync(int productId)
	{
		logger.LogInformation("Getting variant by product: {ProductId}", productId);

		try
		{
			var variants = await context.ProductVariants
				.AsNoTracking()
				.Where(v => v.ProductId == productId)
				.Select(v => new ProductVariantResponseDto
				{
					Id = v.Id,
					Name = v.Name,
					ProductId = v.ProductId,
					PriceModifier = v.PriceModifier
				})
				.ToArrayAsync();

			if (variants.Length == 0)
			{
				logger.LogWarning("No variant found for product: {ProductId}", productId);
			}
			else
			{
				logger.LogInformation("Found {Count} variant products", variants.Length);
			}

			return variants;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to get variant by product: {ProductId}", productId);
			throw;
		}
	}

	public async Task<ProductVariantResponseDto> CreateProductVariantAsync(ProductVariantCreateDto dto)
	{
		logger.LogInformation("Creating a new product variant: {VariantName}", dto.Name);

		try
		{
			var productExists = await context.Products.AnyAsync(p => p.Id == dto.ProductId);
			if (!productExists)
			{
				logger.LogWarning("Product with ID {ProductId} not found", dto.ProductId);
				throw new Exception($"Product with ID {dto.ProductId} not found");
			}

			var variant = new ProductVariant
			{
				Name = dto.Name,
				Description = dto.Description,
				ProductId =dto.ProductId,
				PriceModifier = dto.PriceModifier,
			};

			context.ProductVariants.Add(variant);
			await context.SaveChangesAsync();

			logger.LogInformation("Successfully created product variant: {VariantId}", variant.Id);

			return new ProductVariantResponseDto
			{
				Id = variant.Id,
				ProductId = variant.ProductId,
				Name = variant.Name,
				PriceModifier = variant.PriceModifier,
			};
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to create product variant: {VariantName}", dto.Name);	
			throw;
		}
	}

	public async Task UpdateProductVariantAsync(ProductVariantUpdateDto dto)
	{
		logger.LogInformation("Updating product variant: {VariantName}", dto.Name);

		try
		{
			var variant = await context.ProductVariants
				.FirstOrDefaultAsync(p => p.Id == dto.Id);

			if (variant is null)
			{
				logger.LogWarning("Product variant with ID {VariantId} not found", dto.Id);
				return;
			}

			variant.Name = dto.Name ?? variant.Name;
			variant.Description = dto.Description ?? variant.Description;
			variant.PriceModifier = dto.PriceModifier?? variant.PriceModifier;

			if (variant.ProductId != dto.ProductId)
			{
				var productExists = await context.Products.AnyAsync(p => p.Id == dto.ProductId);
				if (!productExists)
				{
					logger.LogWarning("Product with ID {ProductId} not found", variant.ProductId);
					throw new Exception($"Product with ID {variant.ProductId} not found");
				}
		
				variant.ProductId = dto.ProductId;
			}

			await context.SaveChangesAsync();

			logger.LogInformation("Successfully updated product variant: {VariantId}", dto.Id);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to update product variant: {VariantId}", dto.Id);
			throw;
		}
	}

	public async Task DeleteProductVariantAsync(int id)
	{
		logger.LogInformation("Deleting product variant: {VariantId}", id);
		var variant = new ProductVariant { Id = id };

		// Potentially remove references from combos when variant is deleted?
		try
		{
			context.ProductVariants.Remove(variant);
			await context.SaveChangesAsync();

			logger.LogInformation("Successfully deleted product variant: {VariantId}", id);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to delete product variant: {VariantId}", id);
			throw;
		}
	}
	#endregion

	#region Ingredient
	public async Task<IngredientResponseDto?> GetIngredientByIdAsync(int id)
	{
		logger.LogInformation("Fetching ingredient with ID: {IngredientId}", id);

		try
		{
			var product = await context.Ingredients
				.AsNoTracking()
				.Where(i => i.Id == id)
				.Select(p => new IngredientResponseDto
				{
					Id = p.Id,
					Name = p.Name,
					PriceModifier = p.PriceModifier
				})
				.FirstOrDefaultAsync();

			if (product is null)
			{
				logger.LogError("Ingredient with ID: {IngredientId} was not found", id);
				return null;
			}

			logger.LogInformation("Ingredient with ID: {IngredientId} has been found", id);
			return product;

		}	
		catch (Exception ex)
		{
			logger.LogError(ex, "Error fetching ingredient with ID: {IngredientId}", id);
			throw;
		}
	}
	
	public async Task<IngredientResponseDto[]> GetIngredientsAsync(int limit = 5, int offset = 0)
	{
		logger.LogInformation("Fetching ingredients with limit {Limit} and offset {Offset}", limit, offset);

		try
		{
			var ingredients = await context.Ingredients
				.AsNoTracking()
				.OrderBy(i => i.Name)
				.Skip(offset)
				.Take(limit)
				.Select(i => new IngredientResponseDto
				{
					Id = i.Id,
					Name = i.Name,
					PriceModifier = i.PriceModifier,
				})
				.ToArrayAsync();

			if (ingredients.Length == 0)
				logger.LogWarning("No ingredients found");
			else
				logger.LogInformation("Fetched {Count} ingredients", ingredients.Length);

			return ingredients;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error fetching ingredients with limit {Limit} and offset {Offset}", limit, offset);
			throw;
		}
	}

	public async Task<IngredientResponseDto> CreateIngredientAsync(IngredientCreateDto dto)
	{
		logger.LogInformation("Creating a new ingredient: {IngredientName}", dto.Name);

		try
		{
			var ingredient = new Ingredient
			{
				Name = dto.Name,
				PriceModifier = dto.PriceModifier,
			};

			context.Ingredients.Add(ingredient);
			await context.SaveChangesAsync();

			logger.LogInformation("Successfully created ingredient: {IngredientName}", ingredient.Id);

			return new IngredientResponseDto
			{
				Id = ingredient.Id,
				Name = ingredient.Name,
				PriceModifier = ingredient.PriceModifier
			};
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to create ingredient: {IngredientName}", dto.Name);	
			throw;
		}
	}

	public async Task UpdateIngredientAsync(IngredientUpdateDto dto)
	{
		logger.LogInformation("Updating ingredient: {IngredientName}", dto.Name);

		try
		{
			var ingredient = await context.Ingredients
				.FirstOrDefaultAsync(p => p.Id == dto.Id);

			if (ingredient is null)
			{
				logger.LogWarning("Ingredient with ID {IngredientId} not found", dto.Id);
				return;
			}

			ingredient.Name = dto.Name ?? ingredient.Name;
			ingredient.PriceModifier = dto.PriceModifier ?? ingredient.PriceModifier;

			await context.SaveChangesAsync();

			logger.LogInformation("Successfully updated ingredient: {IngredientId}", dto.Id);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to update ingredient: {IngredientId}", dto.Id);
			throw;
		}
	}

	public async Task DeleteIngredientByIdAsync(int id)
	{
		logger.LogInformation("Deleting ingredient: {IngredientId}", id);
		var ingredient = new Ingredient { Id = id };

		try
		{
			context.Ingredients.Remove(ingredient);
			await context.SaveChangesAsync();

			logger.LogInformation("Successfully deleted ingredient: {IngredientId}", ingredient.Id);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to delete ingredient: {Id}", id);
			throw;
		}
	}
	#endregion

	#region Category
	public async Task<TagMinimalResponseDto?> GetTagsByIdAsync(int id)
	{
		logger.LogInformation("Fetching category: {CategoryId}", id);

		try
		{
			var category = await context.Tags
				.Select(c => new TagMinimalResponseDto
				{
					Id = c.Id,
					Name = c.Name
				})
				.FirstOrDefaultAsync(c => c.Id == id);

			if (category is null)
			{
				logger.LogWarning("Category with ID {CategoryId} not found", id);
				return null;
			}

			logger.LogInformation("Successfully fetched category: {CategoryId}", category.Id);
			return category;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to fetch category: {CategoryId}", id);
			throw;
		}
	}

	public async Task<TagMinimalResponseDto[]> GetTagsAsync()
	{
		logger.LogInformation("Fetching categories all categories");

		try
		{
			var categories = await context.Tags
				.AsNoTracking()
				.OrderBy(c => c.Id)
				.Select(p => new TagMinimalResponseDto
				{
					Id = p.Id,
					Name = p.Name,
				})
				.ToArrayAsync();

			if (categories.Length == 0)
				logger.LogWarning("No categories found");
			else
				logger.LogInformation("Fetched {Count} categories", categories.Length);

			return categories;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error fetching categories");
			throw;
		}
	}

	public async Task<TagMinimalResponseDto> CreateCategoryAsync(TagCreateDto dto)
	{
		logger.LogInformation("Creating category: {CategoryName}", dto.Name);

		var category = new Tag { Name = dto.Name };

		try
		{
			context.Tags.Add(category);
			await context.SaveChangesAsync();

			logger.LogInformation("Successfully created new category: {CategoryName}", dto.Name);

			return new TagMinimalResponseDto
			{
				Id = category.Id,
				Name = category.Name
			};
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to create category: {CategoryName}", dto.Name);
			throw;
		}
	}

	public async Task UpdateCategoryAsync(TagUpdateDto dto)
	{
		logger.LogInformation("Updating category: {CategoryId}", dto.Id);

		try
		{
			var category = await context.Tags
				.FirstOrDefaultAsync(c => c.Id == dto.Id);

			if (category is null)
			{
				logger.LogWarning("Category with ID {CategoryId} not found", dto.Id);
				return;
			}

			category.Name = dto.Name ?? category.Name;

			await context.SaveChangesAsync();
			logger.LogInformation("Successfully updated category: {CategoryId}", dto.Id);
		}
		catch (Exception e)
		{
			logger.LogError(e, "Failed to update category: {CategoryId}", dto.Id);
			throw;
		}
	}

	public async Task DeleteCategoryAsync(int id)
	{
		logger.LogInformation("Deleting category");
		var category = new Tag { Id = id };

		try
		{
			context.Tags.Remove(category);
			await context.SaveChangesAsync();

			logger.LogInformation("Successfully deleted category: {CategoryId}", category.Id);
		}
		catch (Exception e)
		{
			logger.LogError(e, "Failed to delete category: {CategoryId}", category.Id);
			throw;
		}
	}
	#endregion
}