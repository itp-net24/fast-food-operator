using FastFoodOperator.Api.Data;
using FastFoodOperator.Api.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FastFoodOperator.Tests;

[TestClass]
public class ProductServiceBaseTest
{
    protected AppDbContext Context;
    protected ProductService Service;
    
    private readonly SqliteConnection _connection;
    private readonly ILogger<ProductService> _logger;
    
    public ProductServiceBaseTest()
    {
        _logger = LoggerFactory.Create(builder =>
        {
            builder.AddDebug();
        }).CreateLogger<ProductService>();
        
        _connection = new SqliteConnection("DataSource=:memory:");
        
        Context = null!;
        Service = null!;
    }

    [TestInitialize]
    public void Setup()
    {
        SQLitePCL.Batteries.Init();
        _connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        Context = new AppDbContext(options);
        Context.Database.EnsureCreated();
        
        Service = new ProductService(Context, _logger);
    }

    [TestCleanup]
    public void Cleanup()
    {
        Context.Dispose();
        _connection.Dispose();
    }
}