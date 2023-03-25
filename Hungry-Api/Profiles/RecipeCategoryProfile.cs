using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;

namespace Hungry_Api.Profiles
{
    public class RecipeCategoryProfile:Profile
    {
        public RecipeCategoryProfile()
        {
            CreateMap<RecipeCategory, RecipeCategoryDTO>();
            CreateMap<RecipeCategoryDTO, RecipeCategory>();
        }
    }
}
