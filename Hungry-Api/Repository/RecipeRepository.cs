using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Hungry_Api.Repository
{
    public class RecipeRepository:BaseRepository<Recipe>,IRecipeRepository
    {
        public RecipeRepository(HungryDbContext context) : base(context) { }

        public Task<Recipe> GetRecipeById(int id)
        {
            var recipe = _dbSet.SingleAsync(r => r.RecipeId== id);
            return recipe;
        }
    }
}
