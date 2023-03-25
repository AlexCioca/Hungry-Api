using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;

namespace Hungry_Api.Profiles
{
    public class UserRecipeProfile:Profile
    {
        public UserRecipeProfile() {
            CreateMap<UserRecipe, UserRecipeDTO>();
            CreateMap<UserRecipeDTO, UserRecipe>();
        }
    }
}
