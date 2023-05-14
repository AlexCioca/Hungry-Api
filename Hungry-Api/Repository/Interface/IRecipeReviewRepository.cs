using Hungry_Api.DbModels;

namespace Hungry_Api.Repository.Interface
{
    public interface IRecipeReviewRepository:IBaseRepository<RecipeReview>
    {
        Task<ICollection<RecipeReview>> GetReviewsForRecipe(int recipeId);
        Task<double> GetRecipeRating(int recipeId);

    }
}
