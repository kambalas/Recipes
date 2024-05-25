using Microsoft.EntityFrameworkCore;
using RecipesAPI.Models;
using System.Reflection.Emit;

namespace RecipesAPI.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Ingredients)
                .WithMany(i => i.Recipes)
                .UsingEntity<RecipeIngredient>();

            modelBuilder.Entity<Recipe>()
                .Property(p => p.Version)
                .IsConcurrencyToken()
                .IsRowVersion();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Recipes)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }


        public DbContext Instance => this;

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredient { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Step> Steps { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
