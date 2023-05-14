using Hungry_Api.DbModels;

namespace Hungry_Api.Repository.Interface
{
    public interface IRecipeRepository:IBaseRepository<Recipe>
    {
        Task<Recipe> GetRecipeById(int id);
        Task<ICollection<Recipe>> GetRecipesForUser(int id);
        Task<ICollection<string>> GetAllRecipesNames();
        Task<ICollection<Recipe>> GetRecipeSearch(string search);
        Task<ICollection<Recipe>> GetRecipeBasedOnCategory(int? categoryId, string recipeName);
        Task<ICollection<Recipe>> GetRecipesOnFolloewdScroll(User user, int number);
        Task<ICollection<Recipe>> GetLikedRecipesForUser(int userId);
        Task<ICollection<Recipe>> GetMostLikedRecipes();
        Task<ICollection<Recipe>> GetRecomandedRecipes(int userId);
        Task<ICollection<Recipe>> GetBestRatingReviews();
    }
}
