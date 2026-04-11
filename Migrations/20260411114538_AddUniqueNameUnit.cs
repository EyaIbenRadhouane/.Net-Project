using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recette.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueNameUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CaloriesRecipe",
                table: "Recipes",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_Name_Unit",
                table: "Ingredients",
                columns: new[] { "Name", "Unit" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ingredient_Name_Unit",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "CaloriesRecipe",
                table: "Recipes");
        }
    }
}
