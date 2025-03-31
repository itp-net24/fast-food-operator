using Microsoft.EntityFrameworkCore;
using FastFoodOperator.Api.Data.Migrations;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace FastFoodOperator.Api.Data.Models
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : DbContext(options)
    {
        public DbSet<Product> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductIngredient> ItemIngredients {get;set;}
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(e => e.Ingredients)
                .WithMany(e => e.Items)
                .UsingEntity<ProductIngredient>();
        }

    }


}
