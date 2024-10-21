using Microsoft.EntityFrameworkCore;
using OnlineShop.API.Models.Domain;

namespace OnlineShop.API.Data
{
    public class WarehouseDButils : DbContext
    {
        public WarehouseDButils(DbContextOptions<WarehouseDButils> options) : base(options)
        {
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
