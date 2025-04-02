using FastFoodOperator.Api.Data;

namespace FastFoodOperator.Api.Services;

public class ProductService(AppDbContext context, ILogger<ProductService> logger)
{
	private readonly AppDbContext _context = context;
	private readonly ILogger<ProductService> _logger = logger;
}