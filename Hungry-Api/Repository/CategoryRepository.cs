﻿using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;

namespace Hungry_Api.Repository
{
    public class CategoryRepository:BaseRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(HungryDbContext context) : base(context) { }

        public async Task<Category> GetById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.CategoryId == id);
        }
    }
}
