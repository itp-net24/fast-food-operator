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

    public class Item
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal BasePrice { get; set; }
        public List<Ingredient>? Ingredients { get;}
        public List<ItemIngredient>? ItemIngredients { get; }
        public required Category ItemCategory {get;set;}
    }
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }

    }
    public class ItemIngredient
    {
        public int? IngredientId { get; set; }
        public int? ItemId { get; set; }
        public required bool Requried { get; set; }
    }
    public class Ingredient
    {   
        public int Id { get; set; }
        public required string Name { get; set; }
        public required decimal PriceModefier { get; set; }
        public List<Item>? Items { get; }
        public List<ItemIngredient>? ItemIngredients { get; }
    }


}
