using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;

namespace Hungry_Api.Profiles
{
    public class UserFollowerProfile:Profile
    {
       public UserFollowerProfile()
        {
            CreateMap<UserFollower, UserFollowerDTO>();
            CreateMap<UserFollowerDTO, UserFollower>();
        }
    }
}
