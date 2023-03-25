using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;

namespace Hungry_Api.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile() {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO,User>();  
        }
    }
}
