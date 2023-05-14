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
            var recipeSearch=_dbSet.Where(data=>data.CategoryId== categoryId && data.RecipeId==recipeId).ToList();
         
            return recipeSearch;

        }
        public async Task<Category> GetCategoryForRecipe(int recipeId)
        {
            var category = _dbSet
            .Where(rc => rc.RecipeId == recipeId)
            .Select(rc => rc.Category)
            .FirstOrDefault();

            return category;
        }
        public async Task<RecipeCategory> GetSingleRecipeCategory(int categoryId, int recipeId)
        {
            var recipeCategory = _dbSet.FirstOrDefault(x => x.CategoryId.Equals(categoryId) && x.RecipeId.Equals(recipeId));

            return recipeCategory;
        }

        public async Task<RecipeCategory> GetRecipeCategoryForRecipe(int recipeId)
        {
            var recipeSearch = _dbSet.FirstOrDefault(data => data.RecipeId==recipeId);

            return recipeSearch;
        }
    }
}
