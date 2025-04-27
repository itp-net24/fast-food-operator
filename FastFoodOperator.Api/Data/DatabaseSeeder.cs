using FastFoodOperator.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastFoodOperator.Api.Data
{
	public static class DatabaseSeeder
	{
		public static void Seed(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "Burgers" },
				new Category { Id = 2, Name = "Drinks" },
				new Category { Id = 3, Name = "Sides" }
			);

			modelBuilder.Entity<Ingredient>().HasData(
				new Ingredient { Id = 1, Name = "Lettuce", PriceModifier = 0.50m },
				new Ingredient { Id = 2, Name = "Tomato", PriceModifier = 0.50m },
				new Ingredient { Id = 3, Name = "Pickles", PriceModifier = 0.25m }
			);
			
			modelBuilder.Entity<Product>().HasData(
				new Product { Id = 1, Name = "Cheeseburger", Description = "A classic cheeseburger", BasePrice = 14.99m, CategoryId = 1 },
				new Product { Id = 2, Name = "Big Mac", Description = "A classic big mac", BasePrice = 29.99m, CategoryId = 1 },
				new Product { Id = 3, Name = "Coke", Description = "Refreshing soda", BasePrice = 1.99m, CategoryId = 2 },
				new Product { Id = 4, Name = "Pepsi", Description = "Refreshing soda", BasePrice = 1.99m, CategoryId = 2 },
				new Product { Id = 5, Name = "French Fries", Description = "Crispy golden fries", BasePrice = 2.99m, CategoryId = 3 },
				new Product { Id = 6, Name = "Chicken Nuggets", Description = "Crispy nuggies", BasePrice = 2.99m, CategoryId = 3 }
			);

			modelBuilder.Entity<ProductIngredient>().HasData(
				// Cheeseburger
				new ProductIngredient { ProductId = 1, IngredientId = 1, Required = true },
				new ProductIngredient { ProductId = 1, IngredientId = 2, Required = false },
				
				// Bigmac
				new ProductIngredient { ProductId = 2, IngredientId = 1, Required = true },
				new ProductIngredient { ProductId = 2, IngredientId = 2, Required = false },
				new ProductIngredient { ProductId = 2, IngredientId = 3, Required = false }
			);

			modelBuilder.Entity<ProductVariant>().HasData(
				// Coke
				new ProductVariant { Id = 1, ProductId = 3, Name = "Small" },
				new ProductVariant { Id = 2, ProductId = 3, Name = "Medium", PriceModifier = 4.99m },
				new ProductVariant { Id = 3, ProductId = 3, Name = "Large", PriceModifier = 9.99m },
				
				// Pepsi
				new ProductVariant { Id = 4, ProductId = 4, Name = "Small" },
				new ProductVariant { Id = 5, ProductId = 4, Name = "Medium", PriceModifier = 4.99m },
				new ProductVariant { Id = 6, ProductId = 4, Name = "Large", PriceModifier = 9.99m },
				
				// Fries
				new ProductVariant { Id = 7, ProductId = 5, Name = "Small" },
				new ProductVariant { Id = 8, ProductId = 5, Name = "Medium", PriceModifier = 4.99m },
				new ProductVariant { Id = 9, ProductId = 5, Name = "Large", PriceModifier = 9.99m },
				
				// Nuggies
				new ProductVariant { Id = 10, ProductId = 6, Name = "6" },
				new ProductVariant { Id = 11, ProductId = 6, Name = "8", PriceModifier = 4.99m },
				new ProductVariant { Id = 12, ProductId = 6, Name = "10", PriceModifier = 9.99m }
			);

			modelBuilder.Entity<Combo>().HasData(
				new Combo { Id = 1, Name = "Cheeseburger Combo", BasePrice = 24.99m, MainComboProductId = 1 },
				new Combo { Id = 2, Name = "BigMac Combo", BasePrice = 29.99m, MainComboProductId = 2 }
			);

			// Do NOT set DefaultComboProduct reference in seeding!
			modelBuilder.Entity<ComboGroup>().HasData(
				new ComboGroup { Id = 1, Name = "Drinks" },
				new ComboGroup { Id = 2, Name = "Sides" }
			);
			
			modelBuilder.Entity<ComboProduct>().HasData(
				// Main
				new ComboProduct { Id = 1,  ComboId = 1, ProductId = 1 },
				new ComboProduct { Id = 2,  ComboId = 2, ProductId = 2 },
				
				// Drinks
				new ComboProduct { Id = 3,  ComboGroupId = 1, ProductId = 3 },
				new ComboProduct { Id = 4,  ComboGroupId = 1, ProductId = 4 },
				
				// Sides
				new ComboProduct { Id = 5,  ComboGroupId = 2, ProductId = 5 },
				new ComboProduct { Id = 6,  ComboGroupId = 2, ProductId = 6 }
			);
			
			modelBuilder.Entity<Order>().HasData(
				new Order { Id = 1, OrderNumber = 1001, OrderStatus = true, CustomerNote = "5kg extra onion", CreatedAt = new DateTime(2024, 01, 01, 12, 0, 0, DateTimeKind.Utc) },
				new Order { Id = 2, OrderNumber = 1002, OrderStatus = false, CustomerNote = "no peanuts", CreatedAt = new DateTime(2024, 01, 02, 12, 0, 0, DateTimeKind.Utc) }
			);
			
			modelBuilder.Entity<OrderProduct>().HasData(
				new OrderProduct { OrderId = 1, ProductId = 1, Quantity = 1 },
				new OrderProduct { OrderId = 1, ProductId = 2, Quantity = 2 },
				new OrderProduct { OrderId = 2, ProductId = 3, Quantity = 1 }
			);

			modelBuilder.Entity<OrderCombo>().HasData(
				new OrderCombo { OrderId = 1, ComboId = 1, Quantity = 1 },
				new OrderCombo { OrderId = 2, ComboId = 2, Quantity = 2 }
			);
		}
	}
}
