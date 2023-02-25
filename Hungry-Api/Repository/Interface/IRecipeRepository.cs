using Hungry_Api.DbModels;

namespace Hungry_Api.Repository.Interface
{
    public interface IRecipeRepository:IBaseRepository<Recipe>
    {
        Task<Recipe> GetRecipeById(int id);
    }
}
