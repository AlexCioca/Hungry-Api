using Hungry_Api.DbModels;
using Hungry_Api.DTO;
using Hungry_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Hungry_Api.Repository
{
    public class UserRecipeRepository:BaseRepository<UserRecipe>,IUserRecipeRepository
    {
        public UserRecipeRepository(HungryDbContext context) : base(context) {}

        public async Task<int> GetLikesForRecipe(int recipeId)
        {
            var likes = await _dbSet.Where(x => x.RecipeId == recipeId).CountAsync();
            return likes;
        }

        public async Task<bool> GetRecipeLike(int userId, int recipeId)
        {
            var liked = await _dbSet.AnyAsync(x => x.RecipeId.Equals(recipeId) && x.UserId.Equals(userId));
            return liked;
        }

        public async Task<UserRecipe> GetSingleUserRecipe(int userId, int recipeId)
        {
            var like = await _dbSet.FirstOrDefaultAsync(x => x.UserId.Equals(userId) && x.RecipeId.Equals(recipeId));

            return like;
        }


    }
}
