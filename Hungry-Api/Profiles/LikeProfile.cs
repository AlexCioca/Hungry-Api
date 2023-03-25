using AutoMapper;
using Hungry_Api.DTO;
using Hungry_Api.DbModels;

namespace Hungry_Api.Profiles
{
    public class LikeProfile:Profile
    {
        public LikeProfile()
        {
            CreateMap<Like, LikeDTO>();
            CreateMap<LikeDTO, Like>();
        }
    }
}
