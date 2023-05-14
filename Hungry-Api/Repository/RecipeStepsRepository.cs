using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Hungry_Api.Repository
{
    public class RecipeStepsRepository: BaseRepository<RecipeSteps>,IRecipeStepsRepository
    {
        public RecipeStepsRepository(HungryDbContext context):base(context)
        {

        }

        public async Task<ICollection<RecipeSteps>> GetStepsForRecipe(int recipeId)
        {
            var steps = await _dbSet.Where(i => i.RecipeId == recipeId).ToListAsync();
            return steps;
        }

        public async Task AddStepForRecipe(RecipeSteps step)
        {
            await _dbSet.AddAsync(step);
        }
        public async Task DeleteStepForRecipe(RecipeSteps step)
        {
            _dbSet.Remove(step);
        }
    }
}
