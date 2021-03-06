// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MistralRestaurant.API.Data;

namespace MistralRestaurant.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220215194917_InitialMigrationUsers")]
    partial class InitialMigrationUsers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IngredientRecipe", b =>
                {
                    b.Property<int>("IngredientsId")
                        .HasColumnType("int");

                    b.Property<int>("RecipesId")
                        .HasColumnType("int");

                    b.HasKey("IngredientsId", "RecipesId");

                    b.HasIndex("RecipesId");

                    b.ToTable("IngredientRecipe");
                });

            modelBuilder.Entity("MistralRestaurant.API.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IngredientMeasureType")
                        .HasColumnType("int");

                    b.Property<double>("IngredientQuantity")
                        .HasColumnType("float");

                    b.Property<int>("MeasureType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PacketQuantity")
                        .HasColumnType("float");

                    b.Property<double>("PacketQuantityPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IngredientMeasureType = 0,
                            IngredientQuantity = 0.0,
                            MeasureType = 4,
                            Name = "Egg",
                            PacketQuantity = 30.0,
                            PacketQuantityPrice = 6.0
                        },
                        new
                        {
                            Id = 2,
                            IngredientMeasureType = 0,
                            IngredientQuantity = 0.0,
                            MeasureType = 1,
                            Name = "Milk",
                            PacketQuantity = 12.0,
                            PacketQuantityPrice = 14.0
                        },
                        new
                        {
                            Id = 3,
                            IngredientMeasureType = 0,
                            IngredientQuantity = 0.0,
                            MeasureType = 1,
                            Name = "Water",
                            PacketQuantity = 6.0,
                            PacketQuantityPrice = 4.0
                        },
                        new
                        {
                            Id = 4,
                            IngredientMeasureType = 0,
                            IngredientQuantity = 0.0,
                            MeasureType = 0,
                            Name = "Pasta",
                            PacketQuantity = 500.0,
                            PacketQuantityPrice = 1.0
                        },
                        new
                        {
                            Id = 5,
                            IngredientMeasureType = 0,
                            IngredientQuantity = 0.0,
                            MeasureType = 3,
                            Name = "Chees",
                            PacketQuantity = 4.0,
                            PacketQuantityPrice = 33.0
                        },
                        new
                        {
                            Id = 6,
                            IngredientMeasureType = 0,
                            IngredientQuantity = 0.0,
                            MeasureType = 3,
                            Name = "Chicken wings",
                            PacketQuantity = 10.0,
                            PacketQuantityPrice = 35.0
                        },
                        new
                        {
                            Id = 7,
                            IngredientMeasureType = 0,
                            IngredientQuantity = 0.0,
                            MeasureType = 1,
                            Name = "Oil",
                            PacketQuantity = 10.0,
                            PacketQuantityPrice = 33.0
                        },
                        new
                        {
                            Id = 8,
                            IngredientMeasureType = 0,
                            IngredientQuantity = 0.0,
                            MeasureType = 3,
                            Name = "Salt",
                            PacketQuantity = 5.0,
                            PacketQuantityPrice = 6.0
                        },
                        new
                        {
                            Id = 9,
                            IngredientMeasureType = 0,
                            IngredientQuantity = 0.0,
                            MeasureType = 2,
                            Name = "Pappar",
                            PacketQuantity = 500.0,
                            PacketQuantityPrice = 6.0
                        },
                        new
                        {
                            Id = 10,
                            IngredientMeasureType = 0,
                            IngredientQuantity = 0.0,
                            MeasureType = 1,
                            Name = "Olive oil",
                            PacketQuantity = 1.0,
                            PacketQuantityPrice = 8.0
                        },
                        new
                        {
                            Id = 11,
                            IngredientMeasureType = 0,
                            IngredientQuantity = 0.0,
                            MeasureType = 4,
                            Name = "Ham",
                            PacketQuantity = 1.0,
                            PacketQuantityPrice = 0.80000000000000004
                        },
                        new
                        {
                            Id = 12,
                            IngredientMeasureType = 0,
                            IngredientQuantity = 0.0,
                            MeasureType = 3,
                            Name = "Tijesto",
                            PacketQuantity = 15.0,
                            PacketQuantityPrice = 15.0
                        });
                });

            modelBuilder.Entity("MistralRestaurant.API.Models.IngredientRecipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<double>("IngredientQuantity")
                        .HasColumnType("float");

                    b.Property<int>("MeasureType")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("IngredientaAndRecipes");
                });

            modelBuilder.Entity("MistralRestaurant.API.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("ManufacturingPrice")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecipeCategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecipeCategoryId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("MistralRestaurant.API.Models.RecipeCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RecipeCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Pancake"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Cake"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Pizza"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Waffle"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Chicken food"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Salad"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Pasta"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Grill"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Burger"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Fish"
                        });
                });

            modelBuilder.Entity("MistralRestaurant.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IngredientRecipe", b =>
                {
                    b.HasOne("MistralRestaurant.API.Models.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MistralRestaurant.API.Models.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MistralRestaurant.API.Models.IngredientRecipe", b =>
                {
                    b.HasOne("MistralRestaurant.API.Models.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MistralRestaurant.API.Models.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("MistralRestaurant.API.Models.Recipe", b =>
                {
                    b.HasOne("MistralRestaurant.API.Models.RecipeCategory", "RecipeCategory")
                        .WithMany("Recipes")
                        .HasForeignKey("RecipeCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RecipeCategory");
                });

            modelBuilder.Entity("MistralRestaurant.API.Models.RecipeCategory", b =>
                {
                    b.HasOne("MistralRestaurant.API.Models.User", null)
                        .WithMany("RecipeCategories")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MistralRestaurant.API.Models.RecipeCategory", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("MistralRestaurant.API.Models.User", b =>
                {
                    b.Navigation("RecipeCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
