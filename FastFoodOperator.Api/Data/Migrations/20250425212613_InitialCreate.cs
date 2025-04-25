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
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Combos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
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
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BasePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PictureUrl = table.Column<string>(type: "varchar(2048)", unicode: false, maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderCombos",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ComboId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCombos", x => new { x.OrderId, x.ComboId });
                    table.ForeignKey(
                        name: "FK_OrderCombos_Combos_ComboId",
                        column: x => x.ComboId,
                        principalTable: "Combos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
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
                name: "ComboProducts",
                columns: table => new
                {
                    ComboId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductVariantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboProducts", x => new { x.ComboId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ComboProducts_Combos_ComboId",
                        column: x => x.ComboId,
                        principalTable: "Combos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboProducts_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComboProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Burgers" },
                    { 2, "Drinks" },
                    { 3, "Sides" }
                });

            migrationBuilder.InsertData(
                table: "Combos",
                columns: new[] { "Id", "BasePrice", "Name" },
                values: new object[,]
                {
                    { 1, 8.99m, "Cheeseburger Combo" },
                    { 2, 12.99m, "Cheeseburger Combo Deluxe" }
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
                table: "OrderCombos",
                columns: new[] { "ComboId", "OrderId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BasePrice", "CategoryId", "Description", "Name", "PictureUrl" },
                values: new object[,]
                {
                    { 1, 5.99m, 1, "A classic cheeseburger", "Cheeseburger", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRyp3wdwtBsAws86q4u0fyCPj12_SiSf9w6jQ&s" },
                    { 2, 1.99m, 2, "Refreshing soda", "Coke", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRyp3wdwtBsAws86q4u0fyCPj12_SiSf9w6jQ&s" },
                    { 3, 2.99m, 3, "Crispy golden fries", "French Fries", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRyp3wdwtBsAws86q4u0fyCPj12_SiSf9w6jQ&s" },
                    { 4, 5.99m, 1, "Grilled beef patty with melted cheddar, lettuce, tomato & onion", "Classic Cheeseburger", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRyp3wdwtBsAws86q4u0fyCPj12_SiSf9w6jQ&s" },
                    { 5, 7.49m, 1, "Two beef patties, smoked bacon, American cheese, pickles & secret sauce", "Bacon Double Burger", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRyp3wdwtBsAws86q4u0fyCPj12_SiSf9w6jQ&s" },
                    { 6, 6.99m, 1, "Beef patty smothered in sautéed mushrooms and Swiss cheese", "Mushroom Swiss Burger", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRyp3wdwtBsAws86q4u0fyCPj12_SiSf9w6jQ&s" },
                    { 7, 6.79m, 1, "Peppered beef patty with pepper jack cheese, jalapeños & chipotle mayo", "Spicy Jalapeño Burger", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRyp3wdwtBsAws86q4u0fyCPj12_SiSf9w6jQ&s" },
                    { 8, 7.19m, 1, "Beef patty topped with crispy onion rings, cheddar & tangy BBQ sauce", "BBQ Onion Ring Burger", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRyp3wdwtBsAws86q4u0fyCPj12_SiSf9w6jQ&s" },
                    { 9, 6.49m, 1, "House-made black bean patty with avocado, lettuce & pico de gallo", "Black Bean Veggie Burger", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRyp3wdwtBsAws86q4u0fyCPj12_SiSf9w6jQ&s" }
                });

            migrationBuilder.InsertData(
                table: "ComboProducts",
                columns: new[] { "ComboId", "ProductId", "ProductVariantId" },
                values: new object[,]
                {
                    { 1, 1, null },
                    { 2, 1, null }
                });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "OrderId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 1, 2, 2 },
                    { 2, 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "ProductIngredients",
                columns: new[] { "IngredientId", "ProductId", "Required" },
                values: new object[,]
                {
                    { 1, 1, true },
                    { 2, 1, true },
                    { 3, 3, false }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "Description", "Name", "PriceModifier", "ProductId" },
                values: new object[,]
                {
                    { 1, null, "Small", 0m, 2 },
                    { 2, null, "Medium", 1.49m, 2 },
                    { 3, null, "Large", 2.49m, 2 },
                    { 4, null, "Small", 0m, 3 },
                    { 5, null, "Medium", 1.49m, 3 },
                    { 6, null, "Large", 2.49m, 3 }
                });

            migrationBuilder.InsertData(
                table: "ComboProducts",
                columns: new[] { "ComboId", "ProductId", "ProductVariantId" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 1, 3, 4 },
                    { 2, 2, 3 },
                    { 2, 3, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComboProducts_ProductId",
                table: "ComboProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboProducts_ProductVariantId",
                table: "ComboProducts",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCombos_ComboId",
                table: "OrderCombos",
                column: "ComboId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIngredients_IngredientId",
                table: "ProductIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariants",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComboProducts");

            migrationBuilder.DropTable(
                name: "OrderCombos");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "ProductIngredients");

            migrationBuilder.DropTable(
                name: "ProductVariants");

            migrationBuilder.DropTable(
                name: "Combos");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
