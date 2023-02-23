using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;

namespace Hungry_Api.Repository
{
    public class RecipeReviewRepository:BaseRepository<RecipeReview>,IRecipeReviewRepository
    {
        public RecipeReviewRepository(HungryDbContext context):base(context) { }    
    }
}
