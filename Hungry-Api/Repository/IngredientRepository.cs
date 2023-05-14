using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Hungry_Api.Repository
{
    public class IngredientRepository:BaseRepository<Ingredient>,IIngredientRepository
    {
        public IngredientRepository(HungryDbContext context) : base(context) { }

        public async Task<ICollection<Ingredient>> GetIngredientsForRecipe(int recipeId)
        {
            var ingredients =await _dbSet.Where(i => i.RecipeId == recipeId).ToListAsync();
            return ingredients;
        }

        public async Task AddIngredientForRecipe(Ingredient ingredient)
        {
            await _dbSet.AddAsync(ingredient);

        }
        public async Task DeleteIngredientForRecipe(Ingredient ingredient)
        {
            _dbSet.Remove(ingredient);
        }

    }
}
