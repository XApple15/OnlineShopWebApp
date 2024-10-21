using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.API.Data
{
    public class WarehouseDBAuthUtils : IdentityDbContext
    {
        public WarehouseDBAuthUtils(DbContextOptions<WarehouseDBAuthUtils> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            var readerRoleId = "5bf2857f-3d64-447f-824a-b8125a00a014";
            var writerRoleId = "212d6dca-8a13-45cc-9492-919ec0c39e8f";

            var roles = new List<IdentityRole>
            {
                new IdentityRole {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "READER"
                },
                new IdentityRole {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "WRITER"
                }
            };
           modelBuilder.Entity<IdentityRole>().HasData(roles);

           
        }
    }
}
