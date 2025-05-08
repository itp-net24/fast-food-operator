using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFoodOperator.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class BeerAndComboSampleImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Combos",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://images.deliveryhero.io/image/menu-import-gateway-prd/regions/EU/chains/mcd_sweden/81f26208cd24bc87543d25761560d32b.png?width=%s");

            migrationBuilder.UpdateData(
                table: "Combos",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://images.deliveryhero.io/image/menu-import-gateway-prd/regions/EU/chains/mcd_sweden/e09c5023b2af710a69b84051b612953e.png?width=%s");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "A refreshing glass of our in-house beer", "https://i.pinimg.com/736x/dd/73/da/dd73da849ff553b4a75a09cd4159c62e.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Combos",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Combos",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Very  cold", null });
        }
    }
}
