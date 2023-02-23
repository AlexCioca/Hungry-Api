using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;

namespace Hungry_Api.Repository
{
    public class UserRepository:BaseRepository<User>,IUserRepository
    {
        public UserRepository(HungryDbContext context) : base(context) { }
    }
}
