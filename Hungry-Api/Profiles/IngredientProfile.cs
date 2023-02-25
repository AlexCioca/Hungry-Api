using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;
using System.Runtime;

namespace Hungry_Api.Profiles
{
    public class IngredientProfile:Profile
    {
        public IngredientProfile() {
            CreateMap<Ingredient, IngredientDTO>();
            CreateMap<IngredientDTO, Ingredient>();
        }
    }
}
