using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;

namespace Hungry_Api.Repository
{
    public class IngredientRepository:BaseRepository<Ingredient>,IIngredientRepository
    {
        public IngredientRepository(HungryDbContext context) : base(context) { }
    }
}
