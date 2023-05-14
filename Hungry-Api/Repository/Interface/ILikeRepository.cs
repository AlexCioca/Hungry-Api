using Hungry_Api.DbModels;

namespace Hungry_Api.Repository.Interface
{
    public interface ILikeRepository:IBaseRepository<Like>
    {
        Task<Like> GetSingleLike(int userId, int categoryId);
        Task DeleteSingleLike(int userId, int categoryId);
    }
}
