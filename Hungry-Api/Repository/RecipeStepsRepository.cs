using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;

namespace Hungry_Api.Repository
{
    public class RecipeStepsRepository: BaseRepository<RecipeSteps>,IRecipeStepsRepository
    {
        public RecipeStepsRepository(HungryDbContext context):base(context)
        {

        }
    }
}
