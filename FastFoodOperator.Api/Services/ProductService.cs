using FastFoodOperator.Api.Data;
using FastFoodOperator.Api.DTOs.Category;
using FastFoodOperator.Api.DTOs.Combo;
using FastFoodOperator.Api.DTOs.Ingredient;
using FastFoodOperator.Api.DTOs.Product;
using FastFoodOperator.Api.DTOs.ProductVariant;
using FastFoodOperator.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastFoodOperator.Api.Services;

public class ProductService (AppDbContext context, ILogger<ProductService> logger)
{
	#region Combo

	public async Task<ProductResponseDto[]> GetComboByIdAsync(int id)
	{
		logger.LogInformation("Fetching products for combo {ComboId}", id);
		
		try
		{
			var products = await context.ComboProducts
				.AsNoTracking()
				.Where(cp => cp.ComboId == id)
				.Include(cp => cp.Product)
				.Select(cp => new ProductResponseDto
				{
					Id = cp.ProductId,
					Name = cp.Product.Name,
					Description = cp.Product.Description,
					BasePrice = cp.Product.BasePrice,
					PictureUrl = cp.Product.PictureUrl
				})
				.ToArrayAsync();

			if (products.Length == 0)
				logger.LogWarning("No products found for combo {ComboId}", id);
			else
				logger.LogInformation("Fetched {Count} products for combo {ComboId}", products.Length, id);
			
			return products;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error fetching products for combo {ComboId}", id);
			throw;
		}
	}
	
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

	public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
	{
		logger.LogInformation("Fetching product with {product.Id}: ", id);

		try
		{
			var product = await context.Products
				.AsNoTracking()
				.Select(p => new ProductResponseDto 
				{
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    BasePrice = p.BasePrice,
                    CategoryId = p.CategoryId,
					PictureUrl = p.PictureUrl
                })
				.FirstOrDefaultAsync(p => p.Id == id);


			if (product == null)
			{
				logger.LogError($"Product with Id {id} was not found");
				return null;
			}

			logger.LogInformation($"Successfully fetching product: {product.Name} {product.Description}");
            return product;

        }	
		catch (Exception ex)
		{
			logger.LogError(ex, $"Error fetching product with product.Id: {id}");
			throw;
		}
	}

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
					BasePrice = p.BasePrice,
					PictureUrl= p.PictureUrl
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
					BasePrice = p.BasePrice,
					PictureUrl = p.PictureUrl
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

	public async Task<ProductResponseDto> CreateProductAsync(ProductCreateDto dto)
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
				PictureUrl = dto.PictureUrl
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

			return new ProductResponseDto
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				BasePrice = product.BasePrice,
				PictureUrl = product.PictureUrl
			};
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
			product.PictureUrl = dto.PictureUrl ?? product.PictureUrl;

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
					Description = v.Description,
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
				Description = variant.Description,
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
				var productExists = await context.Products.AnyAsync(p => p.Id == variant.ProductId);
				if (!productExists)
				{
					logger.LogWarning("Product with ID {ProductId} not found", variant.ProductId);
					throw new Exception($"Product with ID {variant.ProductId} not found");
				}
		
				variant.ProductId = variant.ProductId;
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
	public async Task<CategoryResponseDto?> GetCategoryByIdAsync(int id)
	{
		logger.LogInformation("Fetching category: {CategoryId}", id);

		try
		{
			var category = await context.Categories
				.Select(c => new CategoryResponseDto
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
	
	public async Task<CategoryResponseDto[]> GetCategoriesAsync(int limit = 5, int offset = 0)
	{
			logger.LogInformation("Fetching categories with limit {Limit} and offset {Offset}", limit, offset);
        
        		try
        		{
        			var categories = await context.Categories
        				.AsNoTracking()
        				.OrderBy(c => c.Id)
        				.Skip(offset)
        				.Take(limit)
        				.Select(p => new CategoryResponseDto
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
        			 logger.LogError(ex, "Error fetching products with limit {Limit} and offset {Offset}", limit, offset);
        			 throw;
        		}
	}

	public async Task CreateCategoryAsync(CategoryCreateDto dto)
	{
		logger.LogInformation("Creating category: {CategoryName}", dto.Name);
		
		var category = new Category { Name = dto.Name };

		try
		{
			context.Categories.Add(category);
			await context.SaveChangesAsync();

			logger.LogInformation("Successfully created new category: {CategoryName}", dto.Name);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to create category: {CategoryName}", dto.Name);
			throw;
		}
	}

	public async Task UpdateCategoryAsync(CategoryUpdateDto dto)
	{
		logger.LogInformation("Updating category: {CategoryId}", dto.Id);

		try
		{
			var category = await context.Categories
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
		var category = new Category { Id = id };

		try
		{
			context.Categories.Remove(category);
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