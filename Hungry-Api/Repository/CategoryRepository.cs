using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Hungry_Api.Repository
{
    public class CategoryRepository:BaseRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(HungryDbContext context) : base(context) { }

        public async Task<Category> GetById(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.CategoryId == id);
        }
    }
}
