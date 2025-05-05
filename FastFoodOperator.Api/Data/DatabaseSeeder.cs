using FastFoodOperator.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastFoodOperator.Api.Data
{
	public static class DatabaseSeeder
	{
		public static void Seed(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Tag>().HasData(
				new Tag { Id = 1, Name = "Burgers", TaxRate = 1.12m },
				new Tag { Id = 2, Name = "Drinks", TaxRate = 1.12m },
				new Tag { Id = 3, Name = "Sides" , TaxRate = 1.12m },
				new Tag { Id = 4, Name = "Desserts" , TaxRate = 1.12m },
				new Tag { Id = 5, Name = "Hot drinks" , TaxRate = 1.12m },
				new Tag { Id = 6, Name = "Combo", TaxRate = 1m },
				new Tag { Id = 7, Name = "Alcohol", TaxRate = 1.25m}
			);

			modelBuilder.Entity<Ingredient>().HasData(
				new Ingredient { Id = 1, Name = "Lettuce", PriceModifier = 0.50m },
				new Ingredient { Id = 2, Name = "Tomato", PriceModifier = 0.50m },
				new Ingredient { Id = 3, Name = "Pickles", PriceModifier = 0.25m }
			);
			
			modelBuilder.Entity<Product>().HasData(
				new Product { Id = 1, Name = "Cheeseburger",	Description = "A classic cheeseburger", BasePrice = 14.99m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-cheeseburger-NEW:nutrition-calculator-tile" },
				new Product { Id = 2, Name = "Big Mac", Description = "A classic big mac", BasePrice = 64.99m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-bigmac-NY2:nutrition-calculator-tile"},
				new Product { Id = 3, Name = "Coca-Cola", Description = "Refreshing soda", BasePrice = 9.99m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-cocacola-original-medium:nutrition-calculator-tile" },
				new Product { Id = 4, Name = "Coca-Cola Zero", Description = "Refreshing soda", BasePrice = 9.99m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-cocacola-original-medium:nutrition-calculator-tile" },
				new Product { Id = 5, Name = "French Fries", Description = "Crispy golden fries", BasePrice = 19.99m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-accessories-french-fries-mellan2:nutrition-calculator-tile" },
				new Product { Id = 6, Name = "Chicken Nuggets", Description = "Crispy nuggies", BasePrice = 39.99m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-chicken-mcnuggets-4p-NEW:nutrition-calculator-tile" },
				new Product { Id = 7,  Name = "Double Cheeseburger",     Description = "Two beef patties with cheese",                BasePrice = 35.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-double-cheeseburger-NEW:nutrition-calculator-tile" },
				new Product { Id = 8,  Name = "Triple Cheeseburger",     Description = "Triple-stacked cheesy classic",              BasePrice = 51.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-triple-cheeseburger-NEW:nutrition-calculator-tile" },
				new Product { Id = 9,  Name = "Quarter Pounder Cheese",  Description = "¼-lb beef, cheese & onions",                 BasePrice = 80.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-qp-cheese-NEW:nutrition-calculator-tile" },
				new Product { Id = 10, Name = "McFeast",                 Description = "Swedish McD classic with fresh veggies",     BasePrice = 77.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-mcfeast-NEW:nutrition-calculator-tile" },
				new Product { Id = 11, Name = "Chili Bearnaise",         Description = "Beef, bacon & chili béarnaise sauce",        BasePrice = 67.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-creamychipotle:nutrition-calculator-tile" },
				new Product { Id = 12, Name = "Tasty Burger",            Description = "Emmentaler, red onion & Tasty sauce",        BasePrice = 79.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-tasty_4_1-burger:nutrition-calculator-tile" },
				new Product { Id = 13, Name = "Tasty Bacon",             Description = "Tasty with crispy bacon",                    BasePrice = 105.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-tasty-bacon_4_1-burger:nutrition-calculator-tile" },
				new Product { Id = 14, Name = "McCrispy",                Description = "Crispy chicken fillet sandwich",             BasePrice = 83.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-chickenfish-mccrispy:nutrition-calculator-tile" },
				new Product { Id = 15, Name = "McCrispy Spicy",          Description = "McCrispy with spicy sambal sauce",           BasePrice = 95.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-chickenfish-mcrispy-spicy2:nutrition-calculator-tile" },
				new Product { Id = 16, Name = "McChicken",               Description = "Classic mayo & lettuce chicken burger",      BasePrice = 63.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-mcchicken-NEW:nutrition-calculator-tile" },
				
				new Product { Id = 17, Name = "Fanta",                   Description = "Orange soda",                                BasePrice = 24.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-fanta-orange-medium:nutrition-calculator-tile" },
				new Product { Id = 18, Name = "Fanta Exotic",            Description = "Tropical fruit soda",                         BasePrice = 24.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-fanta-exotic-medium:nutrition-calculator-tile" },
				new Product { Id = 19, Name = "Sprite Zero Sugar",       Description = "Sugar-free lemon-lime soda",                 BasePrice = 24.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-sprite-zero-medium:nutrition-calculator-tile" },
				new Product { Id = 20, Name = "Latte",                   Description = "McCafé organic latte",                       BasePrice = 41.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-LatteVaniljCaramel:nutrition-calculator-tile" },
				new Product { Id = 21, Name = "Cappuccino",              Description = "Foamy espresso-based coffee",                BasePrice = 41.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-Cappuccino-NEW:nutrition-calculator-tile" },
				new Product { Id = 22, Name = "Espresso",                Description = "Single espresso shot",                       BasePrice = 32.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-Espresso-Cortado-NEW:nutrition-calculator-tile" },
				new Product { Id = 23, Name = "Hot Chocolate",           Description = "Creamy O’Boy hot chocolate",                 BasePrice = 25.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-Choklad:nutrition-calculator-tile" },
				new Product { Id = 24, Name = "God Morgon Äppeljuice",   Description = "Cloudy apple juice",                          BasePrice = 34.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-godmorgon-apple:nutrition-calculator-tile" },
				
				new Product { Id = 25, Name = "Apple Pie",               Description = "Crispy pastry with apple filling",           BasePrice = 18.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-desserts-applepie:nutrition-calculator-tile" },
				new Product { Id = 26, Name = "Sundae Chocolate",        Description = "Soft-serve with hot fudge",                  BasePrice = 24.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-sundae-chocolate:nutrition-calculator-tile" },
				new Product { Id = 27, Name = "McFlurry Oreo",           Description = "Soft-serve blended with Oreo pieces",        BasePrice = 35.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-desserts-mcflurry-oreo:nutrition-calculator-tile" },
				new Product { Id = 28, Name = "Triple Chocolate Cookie", Description = "Rich three-chocolate cookie",                BasePrice = 32.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-desserts-triplechocolatecookie:nutrition-calculator-tile" },
				new Product { Id = 29, Name = "Cinnamon Bun",           Description = "Classic Swedish kanelbulle",                 BasePrice = 40.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-mccafe-kanelbulle:nutrition-calculator-tile" },
				new Product { Id = 30, Name = "Chocolate Muffin",        Description = "Moist chocolate muffin",                     BasePrice = 32.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-mccafe-muffinchoklad:nutrition-calculator-tile" },
				new Product { Id = 31, Name = "MiniDonut Choklad",       Description = "Chocolate-glazed mini donut",                BasePrice = 32.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-mccafe-mini-donutchoklad:nutrition-calculator-tile" },
				new Product { Id = 32, Name = "Apple Slices",            Description = "Fresh apple snack pack",                     BasePrice = 18.00m, ImageUrl = "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-accessories-apples:nutrition-calculator-tile" },
				new Product { Id = 33, Name = "Beer",					Description = "Very  cold",									BasePrice = 40.00m }

			);

			modelBuilder.Entity<ProductTag>().HasData(
				new ProductTag { ProductId = 1, TagId = 1 },
				new ProductTag { ProductId = 2, TagId = 1 },
				new ProductTag { ProductId = 3, TagId = 2 },
				new ProductTag { ProductId = 4, TagId = 2 },
				new ProductTag { ProductId = 5, TagId = 3 },
				new ProductTag { ProductId = 6, TagId = 3 },
				new ProductTag { ProductId = 7, TagId = 1 },
				new ProductTag { ProductId = 8, TagId = 1 },
				new ProductTag { ProductId = 9, TagId = 1 },
				new ProductTag { ProductId = 10, TagId = 1 },
				new ProductTag { ProductId = 11, TagId = 1 },
				new ProductTag { ProductId = 12, TagId = 1 },
				new ProductTag { ProductId = 13, TagId = 1 },
				new ProductTag { ProductId = 14, TagId = 1 },
				new ProductTag { ProductId = 15, TagId = 1 },
				new ProductTag { ProductId = 16, TagId = 1 },
				new ProductTag { ProductId = 17, TagId = 2 },
				new ProductTag { ProductId = 18, TagId = 2 },
				new ProductTag { ProductId = 19, TagId = 2 },
				new ProductTag { ProductId = 20, TagId = 5 },
				new ProductTag { ProductId = 21, TagId = 5 },
				new ProductTag { ProductId = 22, TagId = 5 },
				new ProductTag { ProductId = 23, TagId = 5 },
				new ProductTag { ProductId = 24, TagId = 2 },
				new ProductTag { ProductId = 25, TagId = 4 },
				new ProductTag { ProductId = 26, TagId = 4 },
				new ProductTag { ProductId = 27, TagId = 4 },
				new ProductTag { ProductId = 28, TagId = 4 },
				new ProductTag { ProductId = 29, TagId = 4 },
				new ProductTag { ProductId = 30, TagId = 4 },
				new ProductTag { ProductId = 31, TagId = 4 },
				new ProductTag { ProductId = 32, TagId = 3 },
				new ProductTag { ProductId = 33, TagId = 7 },
				new ProductTag { ProductId = 33, TagId = 2 }
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

			modelBuilder.Entity<ComboTag>().HasData(
				new ComboTag { ComboId = 1, TagId = 1 },
				new ComboTag { ComboId = 1, TagId = 6 },
				new ComboTag { ComboId = 2, TagId = 1 },
				new ComboTag { ComboId = 2, TagId = 6 }
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
				new Order { Id = 1, OrderNumber = 1001, OrderStatus = OrderStatus.Completed, CustomerNote = "5kg extra onion", CreatedAt = new DateTime(2024, 01, 01, 12, 0, 0, DateTimeKind.Utc) },
				new Order { Id = 2, OrderNumber = 1002, OrderStatus = OrderStatus.Created, CustomerNote = "no peanuts", CreatedAt = new DateTime(2024, 01, 02, 12, 0, 0, DateTimeKind.Utc) }
			);
			
			modelBuilder.Entity<OrderProduct>().HasData(
				new OrderProduct { Id = 1, ProductName = "Bajskorv", OrderId = 1, Quantity = 1 },
				new OrderProduct { Id = 2, ProductName = "Skurhinksmilkshake", OrderId = 1, Quantity = 2 },
				new OrderProduct { Id = 3, ProductName = "Pannkakor", OrderId = 2, Quantity = 3 }
			);

			modelBuilder.Entity<OrderCombo>().HasData(
				new OrderCombo { Id = 1,ComboName = "Bajs och kiss", OrderId = 1, Quantity = 1 },
				new OrderCombo { Id = 2, ComboName = "Ägg och bacon", OrderId = 2, Quantity = 2 }
			);
		}
	}
}
