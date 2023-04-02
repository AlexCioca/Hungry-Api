using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;

namespace Hungry_Api.Profiles
{
    public class RecipeStepsProfile:Profile
    {
        public RecipeStepsProfile()
        {
            CreateMap<RecipeSteps, RecipeStepsDTO>();
            CreateMap<RecipeStepsDTO, RecipeSteps>();
        }
    }
}
