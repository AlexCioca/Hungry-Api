using Hungry_Api.DbModels;

namespace Hungry_Api.Repository.Interface
{
    public interface IUserRepository:IBaseRepository<User>
    {
        Task<User> GetUserById(int id);
    }
}
