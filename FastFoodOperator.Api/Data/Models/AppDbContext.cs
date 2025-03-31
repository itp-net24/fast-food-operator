using Microsoft.EntityFrameworkCore;
using FastFoodOperator.Api.Data.Migrations;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace FastFoodOperator.Api.Data.Models
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : DbContext(options)
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ItemIngredient> ItemIngredients {get;set;}
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasMany(e => e.Ingredients)
                .WithMany(e => e.Items)
                .UsingEntity<ItemIngredient>();
        }

    }


}
