using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hungry_Api.Repository
{
    public class RecipeImageRepository:BaseRepository<RecipeImage>,IRecipeImageRepository
    {
        public RecipeImageRepository(HungryDbContext context):base(context)
        {
        }

        public Task<RecipeImage> GetImageById(int id)
        {
            var image = _dbSet.SingleAsync(r => r.RecipeImageId == id);
            return image;
        }
        public async Task<ICollection<RecipeImage>> GetImagesForARecepie(int id)
        {
            var images = _dbSet.Where(x => x.RecipeId == id).ToListAsync();
            return images.Result;
        }
    }
}
