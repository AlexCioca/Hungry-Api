using AutoMapper;
using Hungry_Api.DTO;
using Hungry_Api.DbModels;

namespace Hungry_Api.Profiles
{
    public class RecipeReviewProfile:Profile
    {
        public RecipeReviewProfile()
        {
            CreateMap<RecipeReview, RecipeReviewDTO>();
            CreateMap<RecipeReviewDTO, RecipeReview>();
        }
    }
}
