using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;

namespace Hungry_Api.Repository
{
    public class UserFollowerRepository: BaseRepository<UserFollower>, IUserFollowerRepository
    {
        public UserFollowerRepository(HungryDbContext context) : base(context) { }

        public async Task<ICollection<UserFollower>> GetByFollowerAsync(int id)
        {
            var users = _dbSet.Where(data => data.FollowerId == id);

            return users.ToList();
        }

        public async Task DeleteByIdAndFollowerId(int currentUserId, int followerId)
        {
            var rel = _dbSet.FirstOrDefault(uf=>uf.CurrentUserId==currentUserId && uf.FollowerId==followerId);
            if (rel != null)
            {
                _dbSet.Remove(rel);
            }
        }

    }
}
