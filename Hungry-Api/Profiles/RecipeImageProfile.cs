using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;

namespace Hungry_Api.Profiles
{
    public class RecipeImageProfile:Profile
    {
        public RecipeImageProfile()
        {
            CreateMap<RecipeImage, RecipeImageDTO>();
            CreateMap<RecipeImageDTO, RecipeImage>();
        }
    }
}
