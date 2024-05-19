using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipesAPI.Migrations
{
    public partial class addusertorecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Recipes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Users_UserId",
                table: "Recipes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Users_UserId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Recipes");
        }
    }
}
