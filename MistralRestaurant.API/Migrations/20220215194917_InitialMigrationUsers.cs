using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MistralRestaurant.API.Migrations
{
    public partial class InitialMigrationUsers : Migration
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
                    PacketQuantityPrice = table.Column<double>(type: "float", nullable: false),
                    IngredientQuantity = table.Column<double>(type: "float", nullable: false),
                    IngredientMeasureType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeCategories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufacturingPrice = table.Column<double>(type: "float", nullable: false),
                    RecipeCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_RecipeCategories_RecipeCategoryId",
                        column: x => x.RecipeCategoryId,
                        principalTable: "RecipeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientaAndRecipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    IngredientQuantity = table.Column<double>(type: "float", nullable: false),
                    MeasureType = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientaAndRecipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientaAndRecipes_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientaAndRecipes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientRecipe",
                columns: table => new
                {
                    IngredientsId = table.Column<int>(type: "int", nullable: false),
                    RecipesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientRecipe", x => new { x.IngredientsId, x.RecipesId });
                    table.ForeignKey(
                        name: "FK_IngredientRecipe_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientRecipe_Recipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "IngredientMeasureType", "IngredientQuantity", "MeasureType", "Name", "PacketQuantity", "PacketQuantityPrice" },
                values: new object[,]
                {
                    { 1, 0, 0.0, 4, "Egg", 30.0, 6.0 },
                    { 12, 0, 0.0, 3, "Tijesto", 15.0, 15.0 },
                    { 10, 0, 0.0, 1, "Olive oil", 1.0, 8.0 },
                    { 9, 0, 0.0, 2, "Pappar", 500.0, 6.0 },
                    { 8, 0, 0.0, 3, "Salt", 5.0, 6.0 },
                    { 7, 0, 0.0, 1, "Oil", 10.0, 33.0 },
                    { 11, 0, 0.0, 4, "Ham", 1.0, 0.80000000000000004 },
                    { 5, 0, 0.0, 3, "Chees", 4.0, 33.0 },
                    { 4, 0, 0.0, 0, "Pasta", 500.0, 1.0 },
                    { 3, 0, 0.0, 1, "Water", 6.0, 4.0 },
                    { 2, 0, 0.0, 1, "Milk", 12.0, 14.0 },
                    { 6, 0, 0.0, 3, "Chicken wings", 10.0, 35.0 }
                });

            migrationBuilder.InsertData(
                table: "RecipeCategories",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[,]
                {
                    { 9, "Burger", null },
                    { 1, "Pancake", null },
                    { 2, "Cake", null },
                    { 3, "Pizza", null },
                    { 4, "Waffle", null },
                    { 5, "Chicken food", null },
                    { 6, "Salad", null },
                    { 7, "Pasta", null },
                    { 8, "Grill", null },
                    { 10, "Fish", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientaAndRecipes_IngredientId",
                table: "IngredientaAndRecipes",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientaAndRecipes_RecipeId",
                table: "IngredientaAndRecipes",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientRecipe_RecipesId",
                table: "IngredientRecipe",
                column: "RecipesId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeCategories_UserId",
                table: "RecipeCategories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_RecipeCategoryId",
                table: "Recipes",
                column: "RecipeCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientaAndRecipes");

            migrationBuilder.DropTable(
                name: "IngredientRecipe");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "RecipeCategories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
