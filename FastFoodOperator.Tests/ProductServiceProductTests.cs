using FastFoodOperator.Api.DTOs.Product;
using Microsoft.EntityFrameworkCore;

namespace FastFoodOperator.Tests;

[TestClass]
public class ProductServiceProductTests : ProductServiceBaseTest
{
    [TestMethod]
    public async Task Get_ShouldReturnProductById_WhenProductExists()
    {
        var product = await Service.GetProductByIdAsync(1);
        
        Assert.IsNotNull(product);
    }

    [TestMethod]
    public async Task Get_ShouldReturnNull_WhenProductDoesNotExist()
    {
        var product = await Service.GetProductByIdAsync(9999);
        
        Assert.IsNull(product);
    }
    
    [TestMethod]
    public async Task Get_ShouldReturnPaginatedProducts()
    {
        var products = await Service.GetProductsAsync();
        
        Assert.IsNotNull(products);
        Assert.IsTrue(products.Length > 0);
    }
    
    [TestMethod]
    public async Task? Get_PaginationShouldUseLimit()
    {
        const int limit = 1;
        var products = await Service.GetProductsAsync(limit);
        
        Assert.IsTrue(products.Length == limit);
    }

    [TestMethod]
    public async Task Get_PaginationShouldUseOffset()
    {
        var productCount = await Context.Products.CountAsync();

        var offset = productCount - 1;
        var products = await Service.GetProductsAsync(10, offset);
        
        Assert.IsTrue(products.Length == 1);
    }

    [TestMethod]
    public async Task Create_ShouldCreateProduct_WhenValid()
    {
        var dto = new ProductCreateDto
        {
            Name = "Test",
            Description = "Test",
            BasePrice = 25.24m,
            CategoryId = 1,
        };
        var product = await Service.CreateProductAsync(dto);
        
        Assert.IsNotNull(product);
        Assert.IsTrue(product.Id > 0);
        Assert.AreEqual(dto.Name, product.Name);
        Assert.AreEqual(dto.Description, product.Description);
        Assert.AreEqual(dto.BasePrice, product.BasePrice);
        Assert.AreEqual(dto.CategoryId, product.CategoryId);
    }

    [TestMethod]
    public async Task Create_ShouldNotAddProduct_WhenInvalid()
    {
        var dto = new ProductCreateDto
        {
            Name = null!
        };
        
        await Assert.ThrowsExceptionAsync<DbUpdateException>(() => Service.CreateProductAsync(dto));
    }

    [TestMethod]
    public async Task Create_ShouldAddProductIngredients_WhenIngredientsIncluded()
    {
        var dto = new ProductCreateDto
        {
            Name = "Test",
            Description = "Test",
            BasePrice = 25.24m,
            CategoryId = 1,
            Ingredients =
            [
                new ProductIngredientCreateDto { IngredientId = 1 },
                new ProductIngredientCreateDto { IngredientId = 2 }
            ]
        };
        
        await Service.CreateProductAsync(dto);

        var product = await Context.Products.Include(p => p.ProductIngredients).OrderBy(p => p.Id).LastOrDefaultAsync();
        
        Assert.IsNotNull(product);
        Assert.IsTrue(product.ProductIngredients.Count == dto.Ingredients.Length);
    }

    [TestMethod]
    public async Task Create_TransactionShouldRollback_WhenInvalid()
    {
        var dto = new ProductCreateDto
        {
            Name = "Test",
            Description = "Test",
            BasePrice = 25.24m,
            CategoryId = 1,
            Ingredients =
            [
                new ProductIngredientCreateDto { IngredientId = 1 },
                new ProductIngredientCreateDto { IngredientId = 0 }
            ]
        };

        var initialProductCount = Context.Products.Count();
        
        try
        {
            await Assert.ThrowsExceptionAsync<DbUpdateException>(() => Service.CreateProductAsync(dto));
        }
        catch (Exception)
        {
            var finalProductCount = await Context.Products.CountAsync();
            Assert.AreEqual(initialProductCount, finalProductCount);
        }
    }

    [TestMethod]
    public async Task Update_ShouldUpdateProduct_WhenValidAndExist()
    {
        var dto = new ProductUpdateDto
        {
            Id = 1,
            Name = "Test",
            Description = "Test",
            BasePrice = 25.24m,
        };
        
        await Service.UpdateProductAsync(dto);
        
        var product = await Service.GetProductByIdAsync(dto.Id);
        
        Assert.IsNotNull(product);
        Assert.AreEqual(dto.Name, product.Name);
        Assert.AreEqual(dto.Description, product.Description);
        Assert.AreEqual(dto.BasePrice, product.BasePrice);
    }

    [TestMethod]
    public async Task Delete_ShouldDeleteProduct_WhenExists()
    {
        const int productId = 1;
        var initialProductCount = await Context.Products.CountAsync();

        await Service.DeleteProductAsync(productId);
        
        var finalProductCount = await Context.Products.CountAsync();

        var references = await Context.ProductVariants.Where(v => v.ProductId == productId).CountAsync();
        
        Assert.AreNotEqual(initialProductCount, finalProductCount);
        Assert.AreEqual(references, 0);
    }
}