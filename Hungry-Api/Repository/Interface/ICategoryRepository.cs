using Hungry_Api.DbModels;

namespace Hungry_Api.Repository.Interface
{
    public interface ICategoryRepository:IBaseRepository<Category>
    {
        Task<Category> GetById(int id);

    }
}
