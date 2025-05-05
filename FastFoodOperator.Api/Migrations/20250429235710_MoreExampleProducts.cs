using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FastFoodOperator.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class MoreExampleProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Desserts" },
                    { 5, "Hot drinks" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-cheeseburger-NEW:nutrition-calculator-tile");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BasePrice", "ImageUrl" },
                values: new object[] { 64.99m, "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-bigmac-NY2:nutrition-calculator-tile" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BasePrice", "ImageUrl", "Name" },
                values: new object[] { 9.99m, "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-cocacola-original-medium:nutrition-calculator-tile", "Coca-Cola" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BasePrice", "ImageUrl", "Name" },
                values: new object[] { 9.99m, "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-cocacola-original-medium:nutrition-calculator-tile", "Coca-Cola Zero" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BasePrice", "ImageUrl" },
                values: new object[] { 19.99m, "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-accessories-french-fries-mellan2:nutrition-calculator-tile" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "BasePrice", "ImageUrl" },
                values: new object[] { 39.99m, "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-chicken-mcnuggets-4p-NEW:nutrition-calculator-tile" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BasePrice", "CategoryId", "DefaultVariantId", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 7, 35.00m, 1, null, "Two beef patties with cheese", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-double-cheeseburger-NEW:nutrition-calculator-tile", "Double Cheeseburger" },
                    { 8, 51.00m, 1, null, "Triple-stacked cheesy classic", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-triple-cheeseburger-NEW:nutrition-calculator-tile", "Triple Cheeseburger" },
                    { 9, 80.00m, 1, null, "¼-lb beef, cheese & onions", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-qp-cheese-NEW:nutrition-calculator-tile", "Quarter Pounder Cheese" },
                    { 10, 77.00m, 1, null, "Swedish McD classic with fresh veggies", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-mcfeast-NEW:nutrition-calculator-tile", "McFeast" },
                    { 11, 67.00m, 1, null, "Beef, bacon & chili béarnaise sauce", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-creamychipotle:nutrition-calculator-tile", "Chili Bearnaise" },
                    { 12, 79.00m, 1, null, "Emmentaler, red onion & Tasty sauce", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-tasty_4_1-burger:nutrition-calculator-tile", "Tasty Burger" },
                    { 13, 105.00m, 1, null, "Tasty with crispy bacon", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-tasty-bacon_4_1-burger:nutrition-calculator-tile", "Tasty Bacon" },
                    { 14, 83.00m, 1, null, "Crispy chicken fillet sandwich", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-chickenfish-mccrispy:nutrition-calculator-tile", "McCrispy" },
                    { 15, 95.00m, 1, null, "McCrispy with spicy sambal sauce", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-chickenfish-mcrispy-spicy2:nutrition-calculator-tile", "McCrispy Spicy" },
                    { 16, 63.00m, 1, null, "Classic mayo & lettuce chicken burger", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-burgers-mcchicken-NEW:nutrition-calculator-tile", "McChicken" },
                    { 17, 24.00m, 2, null, "Orange soda", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-fanta-orange-medium:nutrition-calculator-tile", "Fanta" },
                    { 18, 24.00m, 2, null, "Tropical fruit soda", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-fanta-exotic-medium:nutrition-calculator-tile", "Fanta Exotic" },
                    { 19, 24.00m, 2, null, "Sugar-free lemon-lime soda", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-sprite-zero-medium:nutrition-calculator-tile", "Sprite Zero Sugar" },
                    { 24, 34.00m, 2, null, "Cloudy apple juice", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-godmorgon-apple:nutrition-calculator-tile", "God Morgon Äppeljuice" },
                    { 25, 18.00m, 3, null, "Crispy pastry with apple filling", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-desserts-applepie:nutrition-calculator-tile", "Apple Pie" },
                    { 32, 18.00m, 3, null, "Fresh apple snack pack", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-accessories-apples:nutrition-calculator-tile", "Apple Slices" },
                    { 20, 41.00m, 5, null, "McCafé organic latte", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-LatteVaniljCaramel:nutrition-calculator-tile", "Latte" },
                    { 21, 41.00m, 5, null, "Foamy espresso-based coffee", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-Cappuccino-NEW:nutrition-calculator-tile", "Cappuccino" },
                    { 22, 32.00m, 5, null, "Single espresso shot", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-Espresso-Cortado-NEW:nutrition-calculator-tile", "Espresso" },
                    { 23, 25.00m, 5, null, "Creamy O’Boy hot chocolate", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-drinks-Choklad:nutrition-calculator-tile", "Hot Chocolate" },
                    { 26, 24.00m, 4, null, "Soft-serve with hot fudge", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-sundae-chocolate:nutrition-calculator-tile", "Sundae Chocolate" },
                    { 27, 35.00m, 4, null, "Soft-serve blended with Oreo pieces", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-desserts-mcflurry-oreo:nutrition-calculator-tile", "McFlurry Oreo" },
                    { 28, 32.00m, 4, null, "Rich three-chocolate cookie", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-desserts-triplechocolatecookie:nutrition-calculator-tile", "Triple Chocolate Cookie" },
                    { 29, 40.00m, 4, null, "Classic Swedish kanelbulle", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-mccafe-kanelbulle:nutrition-calculator-tile", "Cinnamon Bun" },
                    { 30, 32.00m, 4, null, "Moist chocolate muffin", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-mccafe-muffinchoklad:nutrition-calculator-tile", "Chocolate Muffin" },
                    { 31, 32.00m, 4, null, "Chocolate-glazed mini donut", "https://s7d1.scene7.com/is/image/mcdonalds/mcd-sv-products-mccafe-mini-donutchoklad:nutrition-calculator-tile", "MiniDonut Choklad" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BasePrice", "ImageUrl" },
                values: new object[] { 29.99m, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BasePrice", "ImageUrl", "Name" },
                values: new object[] { 1.99m, null, "Coke" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BasePrice", "ImageUrl", "Name" },
                values: new object[] { 1.99m, null, "Pepsi" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BasePrice", "ImageUrl" },
                values: new object[] { 2.99m, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "BasePrice", "ImageUrl" },
                values: new object[] { 2.99m, null });
        }
    }
}
