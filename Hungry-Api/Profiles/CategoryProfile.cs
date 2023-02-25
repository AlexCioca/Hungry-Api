using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;

namespace Hungry_Api.Profiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile() {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
        }
        
    }
}
