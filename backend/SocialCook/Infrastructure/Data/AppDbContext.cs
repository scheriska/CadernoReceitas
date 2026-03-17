using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialCook.Domain.Entities;

namespace SocialCook.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Recipe> Recipes => Set<Recipe>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();
        public DbSet<RecipeImage> RecipeImages => Set<RecipeImage>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<RecipeCategory> RecipeCategories => Set<RecipeCategory>();
        public DbSet<Beverage> Beverages => Set<Beverage>();
        public DbSet<RecipeBeverage> RecipeBeverages => Set<RecipeBeverage>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RecipeCategory>()
                .HasIndex(rc => new { rc.RecipeId, rc.CategoryId })
                .IsUnique();
            modelBuilder.Entity<RecipeImage>()
                .HasKey(ri => new { ri.RecipeId, ri.Id });
            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientId);
            modelBuilder.Entity<RecipeIngredient>()
                .HasIndex(ri => new { ri.RecipeId, ri.IngredientId })
                .IsUnique();
            modelBuilder.Entity<RecipeBeverage>()
                .HasKey(rb => new { rb.RecipeId, rb.BeverageId });
        }
    }
}