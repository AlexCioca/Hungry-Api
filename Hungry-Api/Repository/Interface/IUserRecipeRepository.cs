using Hungry_Api.DbModels;

namespace Hungry_Api.Repository.Interface
{
    public interface IUserRecipeRepository:IBaseRepository<UserRecipe>
    {

        Task<int> GetLikesForRecipe(int recipeId);
        Task<bool> GetRecipeLike(int userId, int recipeId);
        Task<UserRecipe> GetSingleUserRecipe(int userId, int recipeId);
    }
}
