using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;

namespace Hungry_Api.Repository
{
    public class UserRecipeRepository:BaseRepository<UserRecipe>,IUserRecipeRepository
    {
        public UserRecipeRepository(HungryDbContext context) : base(context) { }
    }
}
