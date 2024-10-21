using OnlineShop.API.Models.Domain;

namespace OnlineShop.API.Repositories
{
    public interface IProductRepository
    {
       
        Task<List<Products>> GetAllAsync(string? filterOn=null,string? filterQuery=null,string? sortBy=null,bool isAscending = true,int pageNumber=1,int pageSize =1000);
        Task<Products> GetByIdAsync(Guid id);
        Task<Products> CreateAsync(Products products);
        Task<Products> UpdateAsync(Guid id, Products products);
        Task<Products> DeleteAsync(Guid id);
    }
}
