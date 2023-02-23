using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;

namespace Hungry_Api.Repository
{
    public class RecipeCategoryRepository:BaseRepository<RecipeCategory>,IRecipeCategoryRepository
    {
        public RecipeCategoryRepository(HungryDbContext context):base(context) { }
    }
}
