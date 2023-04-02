using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Hungry_Api.Repository
{
    public class UserRepository:BaseRepository<User>,IUserRepository
    {
        public UserRepository(HungryDbContext context) : base(context) { }

        public async Task<User> GetUserById(int id)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u=>u.UserId== id);
            return user;
        }
    }
}
