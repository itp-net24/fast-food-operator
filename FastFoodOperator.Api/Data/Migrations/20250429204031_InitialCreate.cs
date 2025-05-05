﻿using System;
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
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    DefaultVariantId = table.Column<int>(type: "int", nullable: true)
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
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Burgers" },
                    { 2, "Drinks" },
                    { 3, "Sides" }
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
                table: "Products",
                columns: new[] { "Id", "BasePrice", "CategoryId", "DefaultVariantId", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, 14.99m, 1, null, "A classic cheeseburger", null, "Cheeseburger" },
                    { 2, 29.99m, 1, null, "A classic big mac", null, "Big Mac" },
                    { 3, 1.99m, 2, null, "Refreshing soda", null, "Coke" },
                    { 4, 1.99m, 2, null, "Refreshing soda", null, "Pepsi" },
                    { 5, 2.99m, 3, null, "Crispy golden fries", null, "French Fries" },
                    { 6, 2.99m, 3, null, "Crispy nuggies", null, "Chicken Nuggets" }
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
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

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
                name: "OrderCombos");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "ProductIngredients");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Ingredients");

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

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}