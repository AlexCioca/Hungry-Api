using Hungry_Api.DbModels;

namespace Hungry_Api.Repository.Interface
{
    public interface IUserFollowerRepository: IBaseRepository<UserFollower>
    {

        Task<ICollection<UserFollower>> GetByFollowerAsync(int id);
        Task DeleteByIdAndFollowerId(int currentUserId,int followerId);

    }
}
