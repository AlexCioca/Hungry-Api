using Hungry_Api.DbModels;
using Hungry_Api.DTO;
using AutoMapper;

namespace Hungry_Api.Profiles
{
    public class RecipeProfile:Profile
    {
        public RecipeProfile()
        {
            CreateMap<Recipe, RecipeDTO>();
            CreateMap<RecipeDTO, Recipe>();
        }
    }
}
