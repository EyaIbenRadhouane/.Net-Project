using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recette.Migrations
{
    /// <inheritdoc />
    public partial class ChangeModelIngred : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Carbohydrates",
                table: "Ingredients",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Ingredients",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Fiber",
                table: "Ingredients",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Lipids",
                table: "Ingredients",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Proteins",
                table: "Ingredients",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Sugar",
                table: "Ingredients",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carbohydrates",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Fiber",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Lipids",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Proteins",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Sugar",
                table: "Ingredients");
        }
    }
}
