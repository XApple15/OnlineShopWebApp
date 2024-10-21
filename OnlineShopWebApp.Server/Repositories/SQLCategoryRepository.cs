using Microsoft.EntityFrameworkCore;
using OnlineShop.API.Data;
using OnlineShop.API.Models.Domain;

namespace OnlineShop.API.Repositories
{
    public class SQLCategoryRepository : ICategoryRepository
    {
        private readonly WarehouseDButils _db;
        public SQLCategoryRepository(WarehouseDButils db)
        {
            this._db = db;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _db.Category.AddAsync(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _db.Category.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await _db.Category.FirstOrDefaultAsync(x => x.Id == id);
        }
      
        public async Task<Category> UpdateAsync(Guid id, Category category)
        {
            var existingCategory = await _db.Category.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCategory == null)
            {
                return null;
            }
            
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            await _db.SaveChangesAsync();
            return existingCategory;
        }
        public async Task<Category> DeleteAsync(Guid id)
        {
            var existingCategory = await _db.Category.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCategory == null)
            {
                return null;
            }
          
            _db.Category.Remove(existingCategory);
            await _db.SaveChangesAsync();
            return existingCategory;
        }
    }
}