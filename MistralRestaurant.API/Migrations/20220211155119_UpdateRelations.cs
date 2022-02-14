using Microsoft.EntityFrameworkCore.Migrations;

namespace MistralRestaurant.API.Migrations
{
    public partial class UpdateRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "RecipeCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeCategories_UserId",
                table: "RecipeCategories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeCategories_Users_UserId",
                table: "RecipeCategories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeCategories_Users_UserId",
                table: "RecipeCategories");

            migrationBuilder.DropIndex(
                name: "IX_RecipeCategories_UserId",
                table: "RecipeCategories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RecipeCategories");
        }
    }
}
