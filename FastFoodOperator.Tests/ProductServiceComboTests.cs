using FastFoodOperator.Api.DTOs.Combo;
using Microsoft.EntityFrameworkCore;

namespace FastFoodOperator.Tests;

[TestClass]
public class ProductServiceComboTests : ProductServiceBaseTest
{
	[TestMethod]
	public async Task Get_ShouldReturnPaginatedCombos()
	{
		var combos = await Service.GetCombosAsync();

		Assert.IsTrue(combos.Length > 0);
	}

	[TestMethod]
	public async Task Get_ShouldReturnCombosOfProduct_WhenProductExists()
	{
		var combo = await Service.GetCombosByProductIdAsync(1);

		Assert.IsTrue(combo.Length > 0);
	}

	[TestMethod]
	public async Task Get_ShouldReturnComboDetails_WhenComboExists()
	{
		var combo = await Service.GetComboByIdAsync(1);

		Assert.IsNotNull(combo);
	}

	[TestMethod]
	public async Task Create_ShouldAddCombo_WhenValid()
	{
		var dto = new ComboCreateDto
		{
			Name = "Test Name",
			BasePrice = 99.99m,
			Products =
			[
				new ComboProductCreateDto { ProductId = 1, DefaultVariantId = 1 },
				new ComboProductCreateDto { ProductId = 2, DefaultVariantId = 2 }
			]
		};

		var combo = await Service.CreateComboAsync(dto);
		Assert.IsNotNull(combo);
		Assert.AreEqual(dto.Name, combo.Name);
		Assert.AreEqual(dto.BasePrice, combo.BasePrice);

		var comboProductsExists = await Context.ComboProducts.Where(cp => cp.ComboId == combo.Id).CountAsync();
		Assert.AreEqual(comboProductsExists, dto.Products.Length);
	}

	[TestMethod]
	public async Task Update_ShouldUpdateCombo_WhenValid()
	{
		var dto = new ComboUpdateDto
		{
			Id = 1,
			Name = "Test Name",
			BasePrice = 99.99m,
		};
		
		await Service.UpdateComboAsync(dto);
		var updatedCombo = await Context.Combos
			.Where(c => c.Id == dto.Id)
			.Select(c => new
			{
				Id = c.Id,
				Name = c.Name,
				BasePrice = c.BasePrice,
			})
			.FirstOrDefaultAsync();
		
		Assert.IsNotNull(updatedCombo);
		Assert.AreEqual(dto.Id, updatedCombo.Id);
		Assert.AreEqual(dto.Name, updatedCombo.Name);
		Assert.AreEqual(dto.BasePrice, updatedCombo.BasePrice);
	}

	[TestMethod]
	public async Task Update_ShouldUpdateComboProducts()
	{
		var dto = new ComboUpdateDto
		{
			Id = 1,
			Name = "Test Name",
			BasePrice = 99.99m,
			Products =
			[
				new ComboProductsUpdateDto { ProductId = 2, DefaultVariantId = 1 },
				new ComboProductsUpdateDto { ProductId = 3, DefaultVariantId = 4 },
			]
		};

		await Service.UpdateComboAsync(dto);
		
		var comboProductsCount = await Context.ComboProducts.Where(cp => cp.ComboId == dto.Id).ToArrayAsync();
		Assert.AreEqual(comboProductsCount.Length, dto.Products.Length);

		for (var i = 0; i < comboProductsCount.Length; i++)
		{
			Assert.AreEqual(comboProductsCount[i].ProductId, dto.Products[i].ProductId);
			Assert.AreEqual(comboProductsCount[i].ProductVariantId, dto.Products[i].DefaultVariantId);
		}
	}
	
	[TestMethod]
	public async Task Delete_ShouldDeleteComboAndReferences_WhenComboExists()
	{
		const int comboId = 1;
		
		await Service.DeleteComboAsync(comboId);
		
		var combo = await Service.GetComboByIdAsync(comboId);
		Assert.IsNull(combo);

		var comboProducts = await Context.ComboProducts.Where(cp => cp.ComboId == comboId).CountAsync();
		Assert.AreEqual(0, comboProducts);
	}
}