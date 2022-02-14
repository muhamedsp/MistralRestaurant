using MistralRestaurant.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MistralRestaurant.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
             
        }

        public DbSet<User> Users { get; set; }

        public DbSet<RecipeCategory> RecipeCategories { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeCategory>().HasData(
                new RecipeCategory { Id = 1, Name = "Pancake" },
                new RecipeCategory { Id = 2, Name = "Cake" },
                new RecipeCategory { Id = 3, Name = "Pizza" },
                new RecipeCategory { Id = 4, Name = "Waffle" },
                new RecipeCategory { Id = 5, Name = "Chicken food" },
                new RecipeCategory { Id = 6, Name = "Salad" },
                new RecipeCategory { Id = 7, Name = "Pasta" },
                new RecipeCategory { Id = 8, Name = "Waffle" },
                new RecipeCategory { Id = 9, Name = "Burger" },
                new RecipeCategory { Id = 10, Name = "Fish" }
            );

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "Egg", MeasureType = MeasureType.unit, PacketQuantity = 30, PacketQuantityPrice = 6},
                new Ingredient { Id = 2, Name = "Milk", MeasureType = MeasureType.L, PacketQuantity = 12, PacketQuantityPrice = 14 },
                new Ingredient { Id = 3, Name = "Water", MeasureType = MeasureType.L, PacketQuantity = 6, PacketQuantityPrice = 4},
                new Ingredient { Id = 4, Name = "Pasta", MeasureType = MeasureType.mL, PacketQuantity = 500, PacketQuantityPrice = 1 },
                new Ingredient { Id = 5, Name = "Chees", MeasureType = MeasureType.Kg, PacketQuantity = 4, PacketQuantityPrice = 33 },
                new Ingredient { Id = 6, Name = "Chicken wings", MeasureType = MeasureType.Kg, PacketQuantity = 10, PacketQuantityPrice = 35 },
                new Ingredient { Id = 7, Name = "Oil", MeasureType = MeasureType.L, PacketQuantity = 10, PacketQuantityPrice = 33 },
                new Ingredient { Id = 8, Name = "Salt", MeasureType = MeasureType.Kg, PacketQuantity = 5, PacketQuantityPrice = 6 },
                new Ingredient { Id = 9, Name = "Pappar", MeasureType = MeasureType.g, PacketQuantity = 500, PacketQuantityPrice = 6 },
                new Ingredient { Id = 10, Name = "Olive oil", MeasureType = MeasureType.L, PacketQuantity = 1, PacketQuantityPrice = 8 },
                new Ingredient { Id = 11, Name = "Ham", MeasureType = MeasureType.unit, PacketQuantity = 1, PacketQuantityPrice = 0.8 }
            );
        }
    }
}
