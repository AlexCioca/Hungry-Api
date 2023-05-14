using Hungry_Api.DbModels;

namespace Hungry_Api.Repository.Interface
{
    public interface IRecipeCategoryRepository:IBaseRepository<RecipeCategory>
    {
        Task<ICollection<RecipeCategory>> GetRecipeBasedOnCategory(int categoryId, int recipeId);
        Task<Category> GetCategoryForRecipe(int recipeId);
        Task<RecipeCategory> GetSingleRecipeCategory(int categoryId, int recipeId);
        Task<RecipeCategory> GetRecipeCategoryForRecipe(int recipeId);
    }
}
