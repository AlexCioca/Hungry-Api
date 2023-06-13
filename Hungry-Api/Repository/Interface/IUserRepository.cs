using Hungry_Api.DbModels;

namespace Hungry_Api.Repository.Interface
{
    public interface IUserRepository:IBaseRepository<User>
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByToken(string token);
        Task<User> GetUserByUsername(string username);
        Task<ICollection<User>> GetPeopleByUsername(string username);
     


    }
}
