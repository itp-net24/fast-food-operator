using FastFoodOperator.Api.DTOs.Category;
using Microsoft.EntityFrameworkCore;

namespace FastFoodOperator.Tests;

[TestClass]
public class ProductServiceCategoryTests : ProductServiceBaseTest
{
	[TestMethod]
	public async Task Get_ShouldReturnCategoryById_WhenCategoryExists()
	{
		var category = await Service.GetCategoryByIdAsync(1);
		
		Assert.IsNotNull(category);
	}

	[TestMethod]
	public async Task Get_ShouldReturnNull_WhenCategoryDoesNotExist()
	{
		var category = await Service.GetCategoryByIdAsync(9999);
		
		Assert.IsNull(category);
	}

	[TestMethod]
	public async Task Get_ShouldReturnPaginatedCategories()
	{
		var categories = await Service.GetCategoriesAsync();
		
		Assert.IsTrue(categories.Length > 0);
	}

    [TestMethod]
    public async Task? Get_PaginationShouldUseLimit()
    {
        const int limit = 1;
        var categories = await Service.GetCategoriesAsync(limit);
        
        Assert.IsTrue(categories.Length == limit);
    }

    [TestMethod]
    public async Task Get_PaginationShouldUseOffset()
    {
        var productCount = await Context.Categories.CountAsync();

        var offset = productCount - 1;
        var products = await Service.GetCategoriesAsync(10, offset);
        
        Assert.IsTrue(products.Length == 1);
    }
    
    [TestMethod]
    public async Task Create_ShouldCreateCategory_WhenValid()
    {
        var dto = new CategoryCreateDto
        {
            Name = "Test",
        };
        
        var category = await Service.CreateCategoryAsync(dto);
        
        Assert.IsNotNull(category);
        Assert.AreEqual(dto.Name, category.Name);
    }

    [TestMethod]
    public async Task Create_ShouldNotAddProduct_WhenInvalid()
    {
        var dto = new CategoryCreateDto
        {
            Name = null!
        };
        
        await Assert.ThrowsExceptionAsync<DbUpdateException>(() => Service.CreateCategoryAsync(dto));
    }
    
    [TestMethod]
    public async Task Update_ShouldUpdateCategory_WhenValidAndExist()
    {
        var dto = new CategoryUpdateDto
        {
            Id = 1,
            Name = "Test",
        };
        
        await Service.UpdateCategoryAsync(dto);
        
        var category = await Service.GetCategoryByIdAsync(dto.Id);
        
        Assert.IsNotNull(category);
        Assert.AreEqual(dto.Name, category.Name);
    }
    
    [TestMethod]
    public async Task Update_ShouldNotUpdateCategory_WhenInvalid()
    {
	    var dto = new CategoryUpdateDto
	    {
		    Id = 1,
		    Name = null!,
	    };
            
	    await Service.UpdateCategoryAsync(dto);
            
	    var category = await Service.GetCategoryByIdAsync(dto.Id);
            
	    Assert.IsNotNull(category);
	    Assert.AreNotEqual(dto.Name, category.Name);
    }
    
    [TestMethod]
    public async Task Delete_ShouldDeleteCategory_WhenExists()
    {
        const int categoryId = 1;
        var initialCount = await Context.Categories.CountAsync();

        await Service.DeleteCategoryAsync(categoryId);
        
        var finalCount = await Context.Categories.CountAsync();

        Assert.AreNotEqual(initialCount, finalCount);
    }
    
    [TestMethod]
    public async Task Delete_ShouldRemoveReferencesOfProduct_WhenSuccessful()
    {
	    const int categoryId = 1;

	    var productBefore = await Context.Products.FirstOrDefaultAsync(p => p.CategoryId == categoryId);
	    Assert.IsNotNull(productBefore);
	    
	    await Service.DeleteCategoryAsync(categoryId);

	    var productAfter = await Context.Products.FirstOrDefaultAsync(p => p.Id == productBefore.Id);
		Assert.IsNotNull(productAfter);
		
		Assert.IsNull(productAfter.CategoryId);
    }
}