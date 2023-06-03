using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Hungry_Api.Repository
{
    public class LikeRepository:BaseRepository<Like>,ILikeRepository
    {
        public LikeRepository(HungryDbContext context):base(context) { }

        public async Task<Like> GetSingleLike(int userId, int categoryId)
        {
            var like = await _dbSet.FirstOrDefaultAsync(x => x.UserId.Equals(userId) && x.CategoryId.Equals(categoryId));
            return like;
        }
        public async Task DeleteSingleLike(int userId, int categoryId)
        {
            var like = _dbSet.FirstOrDefault(x => x.UserId.Equals(userId) && x.CategoryId.Equals(categoryId));
            if (like!=null)
            {
                _dbSet.Remove(like);
            }
        }
    }

}
