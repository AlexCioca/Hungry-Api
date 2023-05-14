using Hungry_Api.DbModels;
using Hungry_Api.DTO;
using Hungry_Api.Repository.Interface;

namespace Hungry_Api.Repository
{
    public class UserRecipeRepository:BaseRepository<UserRecipe>,IUserRecipeRepository
    {
        public UserRecipeRepository(HungryDbContext context) : base(context) {}

        public async Task<int> GetLikesForRecipe(int recipeId)
        {
            var likes = _dbSet.Where(x => x.RecipeId == recipeId).Count();
            return likes;
        }

        public async Task<bool> GetRecipeLike(int userId, int recipeId)
        {
            var liked = _dbSet.Any(x => x.RecipeId.Equals(recipeId) && x.UserId.Equals(userId));
            return liked;
        }

        public async Task<UserRecipe> GetSingleUserRecipe(int userId, int recipeId)
        {
            var like = _dbSet.FirstOrDefault(x => x.UserId.Equals(userId) && x.RecipeId.Equals(recipeId));

            return like;
        }


    }
}
