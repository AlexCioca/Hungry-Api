using Hungry_Api.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace Hungry_Api.Repository.Interface
{
    public interface IRecipeImageRepository:IBaseRepository<RecipeImage>
    {
        Task<ICollection<RecipeImage>> GetImagesForARecepie(int id);
    }
}
