using Microsoft.EntityFrameworkCore;
using OnlineShop.API.Data;
using OnlineShop.API.Models.Domain;

namespace OnlineShop.API.Repositories
{
    public class SQLProductRepositorycs : IProductRepository
    {
        private readonly WarehouseDButils _db;

        public SQLProductRepositorycs(WarehouseDButils db)
        {
            this._db = db;
        }



       public  async Task<List<Products>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true,int pageNumber =1, int pageSize = 1000)
        {
            var allProducts = _db.Products.Include("Category").AsQueryable();


            //Filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if( filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    allProducts = allProducts.Where(x => x.Name.Contains(filterQuery));  
                }
                if( filterOn.Equals("CategoryName", StringComparison.OrdinalIgnoreCase))
                {
                    allProducts = allProducts.Where(x => x.Category.Name.Contains(filterQuery));
                }
            }

            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    allProducts = isAscending ? allProducts.OrderBy(x => x.Name) : allProducts.OrderByDescending(x => x.Name);
                }
                if (sortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    allProducts = isAscending? allProducts.OrderBy(x=>x.Price) : allProducts.OrderByDescending(x=>x.Price);
                }
                
            }

            //Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await allProducts.Skip(skipResults).Take(pageSize).ToListAsync();
        }

      public async  Task<Products> GetByIdAsync(Guid id)
        {
            return await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async  Task<Products> CreateAsync(Products products)
        {
            await _db.Products.AddAsync(products);
            await _db.SaveChangesAsync();
            return products;
        }

        public async Task<Products> UpdateAsync(Guid id, Products products)
        {
            var existingProduct = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);

            if( existingProduct == null)
            {
                return null;
            }
            existingProduct.Name = products.Name;
            existingProduct.Description = products.Description;
            existingProduct.Price = products.Price;
            existingProduct.Quantity = products.Quantity;
            existingProduct.CategoryId = products.CategoryId;
            existingProduct.ProductImageURL = products.ProductImageURL;

            await _db.SaveChangesAsync();
            return existingProduct;
        }

      public async  Task<Products> DeleteAsync(Guid id)
        {
            var existingProduct = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if( existingProduct == null) {
                return null;
            }
            _db.Products.Remove(existingProduct);
            await _db.SaveChangesAsync();
            return existingProduct;
        }

     
    }
}
