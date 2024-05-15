using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipesAPI.Migrations
{
    public partial class addimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeasurementType",
                table: "Ingredients");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Recipes",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeasurementType",
                table: "RecipeIngredient",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "MeasurementType",
                table: "RecipeIngredient");

            migrationBuilder.AddColumn<int>(
                name: "MeasurementType",
                table: "Ingredients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
