using System.Diagnostics;
using FastFoodOperator.Api.DTOs.ProductVariant;
using Microsoft.EntityFrameworkCore;

namespace FastFoodOperator.Tests;

[TestClass]
public class ProductServiceVariantTests : ProductServiceBaseTest
{
	[TestMethod]
	public async Task Get_ShouldReturnVariants_WhenProductExists()
	{
		const int productId = 1;
		
		var variants = await Service.GetVariantsByProductIdAsync(productId);
		var existingVariants = await Context.ProductVariants.Where(v => v.ProductId == productId).ToArrayAsync();
		
		Assert.IsTrue(variants.Length == existingVariants.Length);
	}

	[TestMethod]
	public async Task Get_ShouldReturnEmptyArray_WhenProductDoesNotExist()
	{
		const int productId = 0;
		var variants = await Service.GetVariantsByProductIdAsync(productId);
		
		Assert.IsTrue(variants.Length == 0);
	}

	[TestMethod]
	public async Task Create_ShouldAddVariantToProduct_WhenProductExists()
	{
		var dto = new ProductVariantCreateDto
		{
			ProductId = 1,
			Name = "Test Variant",
			Description = "Test Description",
			PriceModifier = 9.99m
		};

		var variant = await Service.CreateProductVariantAsync(dto);
		
		Assert.IsNotNull(variant);
	}

	[TestMethod]
	public async Task Create_ShouldThrowException_WhenProductDoesNotExist()
	{
			var dto = new ProductVariantCreateDto
    		{
    			ProductId = 0,
    			Name = "Test Variant",
    			Description = "Test Description",
    			PriceModifier = 9.99m
    		};
    
		    await Assert.ThrowsExceptionAsync<Exception>(() => Service.CreateProductVariantAsync(dto));
	}

	[TestMethod]
	public async Task Update_ShouldUpdateVariant_WhenProductExists()
	{
		var dto = new ProductVariantUpdateDto
		{
			Id = 1,
			ProductId = 1,
			Name = "Test Variant",
			Description = "Test Description",
			PriceModifier = 9.99m
		};

		await Service.UpdateProductVariantAsync(dto);

		var variant = await Context.ProductVariants.FirstOrDefaultAsync();
		
		Assert.IsNotNull(variant);
		Assert.AreEqual(dto.ProductId, variant.ProductId);
		Assert.AreEqual(dto.Name, variant.Name);
		Assert.AreEqual(dto.Description, variant.Description);
		Assert.AreEqual(dto.PriceModifier, variant.PriceModifier);
	}

	[TestMethod]
	public async Task Update_ShouldThrowException_WhenNewProductDoesNotExist()
	{
		var dto = new ProductVariantUpdateDto
		{
			Id = 1,
			ProductId = 0,
			Name = "Test Variant",
			Description = "Test Description",
			PriceModifier = 9.99m
		};
    
		await Assert.ThrowsExceptionAsync<Exception>(() => Service.UpdateProductVariantAsync(dto));
	}

	[TestMethod]
	public async Task Delete_ShouldDeleteVariant_WhenVariantExists()
	{
		const int variantId = 1;
		var initialVariantCount = await Context.ProductVariants.CountAsync();
		
		await Service.DeleteProductVariantAsync(variantId);
		
		var finalVariantCount = await Context.ProductVariants.CountAsync();

		Assert.AreNotEqual(initialVariantCount, finalVariantCount);
	}

	[TestMethod]
	public async Task Delete_ShouldDeleteComboReferences_WhenVariantExists()
	{
		const int variantId = 1;
		var initialComboCount = await Context.ComboProducts.Where(cp => cp.ProductVariantId == variantId).CountAsync();
		
		await Service.DeleteProductVariantAsync(variantId);
        
		var finalComboCount = await Context.ComboProducts.Where(cp => cp.ProductVariantId == variantId).CountAsync();
        		
		Assert.AreNotEqual(initialComboCount, finalComboCount);
	}
}