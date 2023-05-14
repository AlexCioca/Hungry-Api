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
        public async Task<User> GetUserByToken(string token)
        {
            var user = await _dbSet.FirstOrDefaultAsync(x => x.Token == token);
            return user;
        }
        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _dbSet.FirstOrDefaultAsync(x => x.Username == username);
            return user;
        }
        public async Task<ICollection<User>> GetPeopleByUsername(string username)
        {
            var users = _dbSet.Where(user => user.Username.ToUpper().StartsWith(username.ToUpper()));
            return users.ToList();
        }

    }
}
