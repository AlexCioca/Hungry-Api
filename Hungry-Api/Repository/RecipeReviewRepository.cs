using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Hungry_Api.Repository
{
    public class RecipeReviewRepository:BaseRepository<RecipeReview>,IRecipeReviewRepository
    {
        public RecipeReviewRepository(HungryDbContext context):base(context) { }

        public async Task<ICollection<RecipeReview>> GetReviewsForRecipe(int recipeId)
        {
            var steps = await _dbSet.Where(i => i.RecipeId == recipeId).ToListAsync();
            return steps;
        }

        public async Task<double> GetRecipeRating(int recipeId)
        {
            var ratings = _dbSet.Where(review => review.RecipeId == recipeId).Select(rev => rev.Rating).Average(); 
            return ratings;
        }
    }
}
