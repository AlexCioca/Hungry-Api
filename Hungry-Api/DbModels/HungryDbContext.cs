using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;

namespace Hungry_Api.DbModels
{
    public class HungryDbContext : DbContext
    {

        public HungryDbContext(DbContextOptions<HungryDbContext> options, IConfiguration configuration) :base(options)
        {
            _configuration = configuration;
            DbConnection = new SqlConnection(this._configuration.GetConnectionString("MyContext"));
        }

        private readonly IConfiguration? _configuration;
        private IDbConnection DbConnection { get; } = new SqlConnection();

        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeReview> RecipeReviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<RecipeCategory> RecipeCategories { get; set; }
        public DbSet<UserRecipe> UserRecipe { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlServer(DbConnection.ConnectionString);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        }

    }
}
