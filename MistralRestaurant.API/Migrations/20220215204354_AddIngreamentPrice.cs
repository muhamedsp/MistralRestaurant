using Microsoft.EntityFrameworkCore.Migrations;

namespace MistralRestaurant.API.Migrations
{
    public partial class AddIngreamentPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "IngredientQuantityPrice",
                table: "Ingredients",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IngredientQuantityPrice",
                table: "Ingredients");
        }
    }
}
