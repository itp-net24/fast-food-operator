using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FastFoodOperator.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Combos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainComboProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PriceModifier = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    CustomerNote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BasePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(2048)", unicode: false, maxLength: 2048, nullable: true),
                    DefaultVariantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderCombos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ComboName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Products = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCombos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderCombos_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Ingredients = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductIngredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Required = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductIngredients", x => new { x.ProductId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_ProductIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductIngredients_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PriceModifier = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComboTag",
                columns: table => new
                {
                    ComboId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboTag", x => new { x.ComboId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ComboTag_Combos_ComboId",
                        column: x => x.ComboId,
                        principalTable: "Combos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTag",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTag", x => new { x.ProductId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ProductTag_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComboGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DefaultComboProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComboGroupCombo",
                columns: table => new
                {
                    ComboGroupsId = table.Column<int>(type: "int", nullable: false),
                    CombosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboGroupCombo", x => new { x.ComboGroupsId, x.CombosId });
                    table.ForeignKey(
                        name: "FK_ComboGroupCombo_ComboGroup_ComboGroupsId",
                        column: x => x.ComboGroupsId,
                        principalTable: "ComboGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboGroupCombo_Combos_CombosId",
                        column: x => x.CombosId,
                        principalTable: "Combos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComboProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComboId = table.Column<int>(type: "int", nullable: true),
                    ComboGroupId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    DefaultVariantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComboProducts_ComboGroup_ComboGroupId",
                        column: x => x.ComboGroupId,
                        principalTable: "ComboGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboProducts_Combos_ComboId",
                        column: x => x.ComboId,
                        principalTable: "Combos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComboProducts_ProductVariants_DefaultVariantId",
                        column: x => x.DefaultVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComboProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ComboGroup",
                columns: new[] { "Id", "DefaultComboProductId", "Name" },
                values: new object[,]
                {
                    { 1, null, "Drinks" },
                    { 2, null, "Sides" }
                });

            migrationBuilder.InsertData(
                table: "Combos",
                columns: new[] { "Id", "BasePrice", "ImageUrl", "MainComboProductId", "Name" },
                values: new object[,]
                {
                    { 1, 24.99m, null, 1, "Cheeseburger Combo" },
                    { 2, 29.99m, null, 2, "BigMac Combo" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name", "PriceModifier" },
                values: new object[,]
                {
                    { 1, "Lettuce", 0.50m },
                    { 2, "Tomato", 0.50m },
                    { 3, "Pickles", 0.25m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CompletedAt", "CreatedAt", "CustomerNote", "OrderNumber", "OrderStatus", "StartedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "5kg extra onion", 1001, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 2, 12, 0, 0, 0, DateTimeKind.Utc), "no peanuts", 1002, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BasePrice", "DefaultVariantId", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, 14.99m, null, "A classic cheeseburger", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-cheeseburger-NEW:nutrition-calculator-tile", "Cheeseburger" },
                    { 2, 64.99m, null, "A classic big mac", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-bigmac-NY2:nutrition-calculator-tile", "Big Mac" },
                    { 3, 9.99m, null, "Refreshing soda", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-cocacola-original-medium:nutrition-calculator-tile", "Coca-Cola" },
                    { 4, 9.99m, null, "Refreshing soda", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-cocacola-original-medium:nutrition-calculator-tile", "Coca-Cola Zero" },
                    { 5, 19.99m, null, "Crispy golden fries", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-accessories-french-fries-mellan2:nutrition-calculator-tile", "French Fries" },
                    { 6, 39.99m, null, "Crispy nuggies", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-chicken-mcnuggets-4p-NEW:nutrition-calculator-tile", "Chicken Nuggets" },
                    { 7, 35.00m, null, "Two beef patties with cheese", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-double-cheeseburger-NEW:nutrition-calculator-tile", "Double Cheeseburger" },
                    { 8, 51.00m, null, "Triple-stacked cheesy classic", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-triple-cheeseburger-NEW:nutrition-calculator-tile", "Triple Cheeseburger" },
                    { 9, 80.00m, null, "¼-lb beef, cheese & onions", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-qp-cheese-NEW:nutrition-calculator-tile", "Quarter Pounder Cheese" },
                    { 10, 77.00m, null, "Swedish McD classic with fresh veggies", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-mcfeast-NEW:nutrition-calculator-tile", "McFeast" },
                    { 11, 67.00m, null, "Beef, bacon & chili béarnaise sauce", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-creamychipotle:nutrition-calculator-tile", "Chili Bearnaise" },
                    { 12, 79.00m, null, "Emmentaler, red onion & Tasty sauce", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-tasty_4_1-burger:nutrition-calculator-tile", "Tasty Burger" },
                    { 13, 105.00m, null, "Tasty with crispy bacon", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-tasty-bacon_4_1-burger:nutrition-calculator-tile", "Tasty Bacon" },
                    { 14, 83.00m, null, "Crispy chicken fillet sandwich", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-chickenfish-mccrispy:nutrition-calculator-tile", "McCrispy" },
                    { 15, 95.00m, null, "McCrispy with spicy sambal sauce", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-chickenfish-mcrispy-spicy2:nutrition-calculator-tile", "McCrispy Spicy" },
                    { 16, 63.00m, null, "Classic mayo & lettuce chicken burger", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-mcchicken-NEW:nutrition-calculator-tile", "McChicken" },
                    { 17, 24.00m, null, "Orange soda", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-fanta-orange-medium:nutrition-calculator-tile", "Fanta" },
                    { 18, 24.00m, null, "Tropical fruit soda", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-fanta-exotic-medium:nutrition-calculator-tile", "Fanta Exotic" },
                    { 19, 24.00m, null, "Sugar-free lemon-lime soda", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-sprite-zero-medium:nutrition-calculator-tile", "Sprite Zero Sugar" },
                    { 20, 41.00m, null, "McCafé organic latte", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-LatteVaniljCaramel:nutrition-calculator-tile", "Latte" },
                    { 21, 41.00m, null, "Foamy espresso-based coffee", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-Cappuccino-NEW:nutrition-calculator-tile", "Cappuccino" },
                    { 22, 32.00m, null, "Single espresso shot", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-Espresso-Cortado-NEW:nutrition-calculator-tile", "Espresso" },
                    { 23, 25.00m, null, "Creamy O’Boy hot chocolate", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-Choklad:nutrition-calculator-tile", "Hot Chocolate" },
                    { 24, 34.00m, null, "Cloudy apple juice", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-godmorgon-apple:nutrition-calculator-tile", "God Morgon Äppeljuice" },
                    { 25, 18.00m, null, "Crispy pastry with apple filling", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-desserts-applepie:nutrition-calculator-tile", "Apple Pie" },
                    { 26, 24.00m, null, "Soft-serve with hot fudge", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-sundae-chocolate:nutrition-calculator-tile", "Sundae Chocolate" },
                    { 27, 35.00m, null, "Soft-serve blended with Oreo pieces", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-desserts-mcflurry-oreo:nutrition-calculator-tile", "McFlurry Oreo" },
                    { 28, 32.00m, null, "Rich three-chocolate cookie", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-desserts-triplechocolatecookie:nutrition-calculator-tile", "Triple Chocolate Cookie" },
                    { 29, 40.00m, null, "Classic Swedish kanelbulle", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-mccafe-kanelbulle:nutrition-calculator-tile", "Cinnamon Bun" },
                    { 30, 32.00m, null, "Moist chocolate muffin", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-mccafe-muffinchoklad:nutrition-calculator-tile", "Chocolate Muffin" },
                    { 31, 32.00m, null, "Chocolate-glazed mini donut", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-mccafe-mini-donutchoklad:nutrition-calculator-tile", "MiniDonut Choklad" },
                    { 32, 18.00m, null, "Fresh apple snack pack", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-accessories-apples:nutrition-calculator-tile", "Apple Slices" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name", "TaxRate" },
                values: new object[,]
                {
                    { 1, "Burgers", 1.12m },
                    { 2, "Drinks", 1.12m },
                    { 3, "Sides", 1.12m },
                    { 4, "Desserts", 1.12m },
                    { 5, "Hot drinks", 1.12m },
                    { 6, "Combo", 0m }
                });

            migrationBuilder.InsertData(
                table: "ComboProducts",
                columns: new[] { "Id", "ComboGroupId", "ComboId", "DefaultVariantId", "ProductId" },
                values: new object[,]
                {
                    { 1, null, 1, null, 1 },
                    { 2, null, 2, null, 2 },
                    { 3, 1, null, null, 3 },
                    { 4, 1, null, null, 4 },
                    { 5, 2, null, null, 5 },
                    { 6, 2, null, null, 6 }
                });

            migrationBuilder.InsertData(
                table: "ComboTag",
                columns: new[] { "ComboId", "TagId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 6 },
                    { 2, 1 },
                    { 2, 6 }
                });

            migrationBuilder.InsertData(
                table: "OrderCombos",
                columns: new[] { "Id", "ComboName", "FinalPrice", "OrderId", "Products", "Quantity" },
                values: new object[,]
                {
                    { 1, "Bajs och kiss", 0m, 1, "", 1 },
                    { 2, "Ägg och bacon", 0m, 2, "", 2 }
                });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "Id", "FinalPrice", "Ingredients", "OrderId", "ProductName", "Quantity" },
                values: new object[,]
                {
                    { 1, 0m, "[]", 1, "Bajskorv", 1 },
                    { 2, 0m, "[]", 1, "Skurhinksmilkshake", 2 },
                    { 3, 0m, "[]", 2, "Pannkakor", 3 }
                });

            migrationBuilder.InsertData(
                table: "ProductIngredients",
                columns: new[] { "IngredientId", "ProductId", "Required" },
                values: new object[,]
                {
                    { 1, 1, true },
                    { 2, 1, false },
                    { 1, 2, true },
                    { 2, 2, false },
                    { 3, 2, false }
                });

            migrationBuilder.InsertData(
                table: "ProductTag",
                columns: new[] { "ProductId", "TagId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 2 },
                    { 5, 3 },
                    { 6, 3 },
                    { 7, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 1 },
                    { 11, 1 },
                    { 12, 1 },
                    { 13, 1 },
                    { 14, 1 },
                    { 15, 1 },
                    { 16, 1 },
                    { 17, 2 },
                    { 18, 2 },
                    { 19, 2 },
                    { 20, 5 },
                    { 21, 5 },
                    { 22, 5 },
                    { 23, 5 },
                    { 24, 2 },
                    { 25, 4 },
                    { 26, 4 },
                    { 27, 4 },
                    { 28, 4 },
                    { 29, 4 },
                    { 30, 4 },
                    { 31, 4 },
                    { 32, 3 }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "Description", "Name", "PriceModifier", "ProductId" },
                values: new object[,]
                {
                    { 1, null, "Small", 0m, 3 },
                    { 2, null, "Medium", 4.99m, 3 },
                    { 3, null, "Large", 9.99m, 3 },
                    { 4, null, "Small", 0m, 4 },
                    { 5, null, "Medium", 4.99m, 4 },
                    { 6, null, "Large", 9.99m, 4 },
                    { 7, null, "Small", 0m, 5 },
                    { 8, null, "Medium", 4.99m, 5 },
                    { 9, null, "Large", 9.99m, 5 },
                    { 10, null, "6", 0m, 6 },
                    { 11, null, "8", 4.99m, 6 },
                    { 12, null, "10", 9.99m, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComboGroup_DefaultComboProductId",
                table: "ComboGroup",
                column: "DefaultComboProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboGroupCombo_CombosId",
                table: "ComboGroupCombo",
                column: "CombosId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboProducts_ComboGroupId",
                table: "ComboProducts",
                column: "ComboGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboProducts_ComboId",
                table: "ComboProducts",
                column: "ComboId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboProducts_DefaultVariantId",
                table: "ComboProducts",
                column: "DefaultVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboProducts_ProductId",
                table: "ComboProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboTag_TagId",
                table: "ComboTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCombos_OrderId",
                table: "OrderCombos",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIngredients_IngredientId",
                table: "ProductIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTag_TagId",
                table: "ProductTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariants",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComboGroup_ComboProducts_DefaultComboProductId",
                table: "ComboGroup",
                column: "DefaultComboProductId",
                principalTable: "ComboProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComboGroup_ComboProducts_DefaultComboProductId",
                table: "ComboGroup");

            migrationBuilder.DropTable(
                name: "ComboGroupCombo");

            migrationBuilder.DropTable(
                name: "ComboTag");

            migrationBuilder.DropTable(
                name: "OrderCombos");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "ProductIngredients");

            migrationBuilder.DropTable(
                name: "ProductTag");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "ComboProducts");

            migrationBuilder.DropTable(
                name: "ComboGroup");

            migrationBuilder.DropTable(
                name: "Combos");

            migrationBuilder.DropTable(
                name: "ProductVariants");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
