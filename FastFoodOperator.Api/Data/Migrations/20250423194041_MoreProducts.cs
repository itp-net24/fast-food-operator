using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FastFoodOperator.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class MoreProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PictureUrl",
                value: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRyp3wdwtBsAws86q4u0fyCPj12_SiSf9w6jQ&s");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "PictureUrl",
                value: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRyp3wdwtBsAws86q4u0fyCPj12_SiSf9w6jQ&s");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "PictureUrl",
                value: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRyp3wdwtBsAws86q4u0fyCPj12_SiSf9w6jQ&s");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BasePrice", "CategoryId", "Description", "Name", "PictureUrl" },
                values: new object[,]
                {
                    { 4, 5.99m, 1, "Grilled beef patty with melted cheddar, lettuce, tomato & onion", "Classic Cheeseburger", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRyp3wdwtBsAws86q4u0fyCPj12_SiSf9w6jQ&s" },
                    { 5, 7.49m, 1, "Two beef patties, smoked bacon, American cheese, pickles & secret sauce", "Bacon Double Burger", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRyp3wdwtBsAws86q4u0fyCPj12_SiSf9w6jQ&s" },
                    { 6, 6.99m, 1, "Beef patty smothered in sautéed mushrooms and Swiss cheese", "Mushroom Swiss Burger", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRyp3wdwtBsAws86q4u0fyCPj12_SiSf9w6jQ&s" },
                    { 7, 6.79m, 1, "Peppered beef patty with pepper jack cheese, jalapeños & chipotle mayo", "Spicy Jalapeño Burger", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRyp3wdwtBsAws86q4u0fyCPj12_SiSf9w6jQ&s" },
                    { 8, 7.19m, 1, "Beef patty topped with crispy onion rings, cheddar & tangy BBQ sauce", "BBQ Onion Ring Burger", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRyp3wdwtBsAws86q4u0fyCPj12_SiSf9w6jQ&s" },
                    { 9, 6.49m, 1, "House-made black bean patty with avocado, lettuce & pico de gallo", "Black Bean Veggie Burger", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRyp3wdwtBsAws86q4u0fyCPj12_SiSf9w6jQ&s" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

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

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PictureUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "PictureUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "PictureUrl",
                value: "");
        }
    }
}
