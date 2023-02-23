using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;

namespace Hungry_Api.Repository
{
    public class RecipeRepository:BaseRepository<Recipe>,IRecipeRepository
    {
        public RecipeRepository(HungryDbContext context) : base(context) { }
    }
}
