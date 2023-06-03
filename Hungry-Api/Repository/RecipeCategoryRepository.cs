using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Hungry_Api.Repository
{
    public class RecipeCategoryRepository:BaseRepository<RecipeCategory>,IRecipeCategoryRepository
    {
        public RecipeCategoryRepository(HungryDbContext context):base(context) { }

        public async Task<ICollection<RecipeCategory>> GetRecipeBasedOnCategory(int categoryId,int recipeId)
        {
            var recipeSearch= await _dbSet.Where(data=>data.CategoryId== categoryId && data.RecipeId==recipeId).ToListAsync();
         
            return recipeSearch;

        }
        public async Task<Category> GetCategoryForRecipe(int recipeId)
        {
            var category =await _dbSet
            .Where(rc => rc.RecipeId == recipeId)
            .Select(rc => rc.Category)
            .FirstOrDefaultAsync();

            return category;
        }
        public async Task<RecipeCategory> GetSingleRecipeCategory(int categoryId, int recipeId)
        {
            var recipeCategory =await _dbSet.FirstOrDefaultAsync(x => x.CategoryId.Equals(categoryId) && x.RecipeId.Equals(recipeId));

            return recipeCategory;
        }

        public async Task<RecipeCategory> GetRecipeCategoryForRecipe(int recipeId)
        {
            var recipeSearch =await _dbSet.FirstOrDefaultAsync(data => data.RecipeId==recipeId);

            return recipeSearch;
        }
    }
}
