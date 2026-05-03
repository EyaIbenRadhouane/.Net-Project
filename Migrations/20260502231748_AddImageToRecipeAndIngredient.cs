using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recette.Migrations
{
    /// <inheritdoc />
    public partial class AddImageToRecipeAndIngredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Recipes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Ingredients",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Ingredients");
        }
    }
}
