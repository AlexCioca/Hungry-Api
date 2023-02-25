using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;

namespace Hungry_Api.Repository
{
    public class LikeRepository:BaseRepository<Like>,ILikeRepository
    {
        public LikeRepository(HungryDbContext context):base(context) { }
    }

}
