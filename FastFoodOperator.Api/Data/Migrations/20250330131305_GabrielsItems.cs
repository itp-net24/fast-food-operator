using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFoodOperator.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class GabrielsItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_ItemIngredients_ItemIngredientId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemIngredients_Items_ItemId",
                table: "ItemIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemIngredients",
                table: "ItemIngredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_ItemIngredientId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "ItemIngredientId",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ItemIngredients",
                newName: "IngredientId");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "ItemIngredients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IngredientId",
                table: "ItemIngredients",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemIngredients",
                table: "ItemIngredients",
                columns: new[] { "IngredientId", "ItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ItemIngredients_Ingredients_IngredientId",
                table: "ItemIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemIngredients_Items_ItemId",
                table: "ItemIngredients",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemIngredients_Ingredients_IngredientId",
                table: "ItemIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemIngredients_Items_ItemId",
                table: "ItemIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemIngredients",
                table: "ItemIngredients");

            migrationBuilder.RenameColumn(
                name: "IngredientId",
                table: "ItemIngredients",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "ItemIngredients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ItemIngredients",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ItemIngredientId",
                table: "Ingredients",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemIngredients",
                table: "ItemIngredients",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_ItemIngredientId",
                table: "Ingredients",
                column: "ItemIngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_ItemIngredients_ItemIngredientId",
                table: "Ingredients",
                column: "ItemIngredientId",
                principalTable: "ItemIngredients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemIngredients_Items_ItemId",
                table: "ItemIngredients",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
