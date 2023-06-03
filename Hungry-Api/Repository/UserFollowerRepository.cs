using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Hungry_Api.Repository
{
    public class UserFollowerRepository: BaseRepository<UserFollower>, IUserFollowerRepository
    {
        public UserFollowerRepository(HungryDbContext context) : base(context) { }

        public async Task<ICollection<UserFollower>> GetByFollowerAsync(int id)
        {
            var users =await _dbSet.Where(data => data.FollowerId == id).ToListAsync();

            return users;
        }

        public async Task DeleteByIdAndFollowerId(int currentUserId, int followerId)
        {
            var rel = _dbSet.FirstOrDefault(uf=>uf.CurrentUserId==currentUserId && uf.FollowerId==followerId);
            if (rel != null)
            {
                _dbSet.Remove(rel);
            }
        }
        public async Task<int> GetNumberOfFollowersForUser(int userId)
        {
            var number = await _dbSet.Where(fol => fol.CurrentUserId==userId).CountAsync();
            return number;
        }
            public async Task<int> GetNumberOfFollowingForUser(int userId)
        {
            var number = await _dbSet.Where(fol => fol.FollowerId == userId).CountAsync();
            return number;
        }
            public async Task<bool> IsFollowing(int currentUserId, int userId)
        {
            var follow = await _dbSet.FirstOrDefaultAsync(fol => fol.CurrentUserId == currentUserId && fol.FollowerId == userId);
            if (follow != null)
                return true;
            else return false;
        }
    }
}
