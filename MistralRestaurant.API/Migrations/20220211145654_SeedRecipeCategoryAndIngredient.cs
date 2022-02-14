using Microsoft.EntityFrameworkCore.Migrations;

namespace MistralRestaurant.API.Migrations
{
    public partial class SeedRecipeCategoryAndIngredient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasureType = table.Column<int>(type: "int", nullable: false),
                    PacketQuantity = table.Column<double>(type: "float", nullable: false),
                    PacketQuantityPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeCategories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "MeasureType", "Name", "PacketQuantity", "PacketQuantityPrice" },
                values: new object[,]
                {
                    { 1, 4, "Egg", 30.0, 6.0 },
                    { 10, 1, "Olive oil", 1.0, 8.0 },
                    { 9, 2, "Pappar", 500.0, 6.0 },
                    { 8, 3, "Salt", 5.0, 6.0 },
                    { 7, 1, "Oil", 10.0, 33.0 },
                    { 11, 4, "Ham", 1.0, 0.80000000000000004 },
                    { 5, 3, "Chees", 4.0, 33.0 },
                    { 4, 0, "Pasta", 500.0, 1.0 },
                    { 3, 1, "Water", 6.0, 4.0 },
                    { 2, 1, "Milk", 12.0, 14.0 },
                    { 6, 3, "Chicken wings", 10.0, 35.0 }
                });

            migrationBuilder.InsertData(
                table: "RecipeCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 9, "Burger" },
                    { 1, "Pancake" },
                    { 2, "Cake" },
                    { 3, "Pizza" },
                    { 4, "Waffle" },
                    { 5, "Chicken food" },
                    { 6, "Salad" },
                    { 7, "Pasta" },
                    { 8, "Waffle" },
                    { 10, "Fish" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "RecipeCategories");
        }
    }
}
