using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recette.Migrations
{
    /// <inheritdoc />
    public partial class AddChefIdToRecipeIngredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChefId",
                table: "Recipes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChefId",
                table: "Ingredients",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChefId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ChefId",
                table: "Ingredients");
        }
    }
}
