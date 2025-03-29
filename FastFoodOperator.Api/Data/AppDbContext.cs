using Microsoft.EntityFrameworkCore;

namespace FastFoodOperator.Api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : DbContext(options)
    {
    }
}
