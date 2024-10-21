using Microsoft.EntityFrameworkCore;
using OnlineShop.API.Data;
using OnlineShop.API.Models.Domain;

namespace OnlineShop.API.Repositories
{
    public class SQLCustomerRepository : ICustomerRepository
    {
        private readonly WarehouseDButils _db;

        public SQLCustomerRepository(WarehouseDButils db)
        {
            this._db = db;
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            await _db.Customer.AddAsync(customer);
            await _db.SaveChangesAsync();
            return customer;
       }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _db.Customer.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            return await _db.Customer.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Customer> UpdateAsync(Guid id, Customer customer)
        {
            var existingCustomer = await _db.Customer.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCustomer != null)
            {
                return null;
            }
            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.Address = customer.Address;

            await _db.SaveChangesAsync();
            return existingCustomer;
            
        }

        public async Task<Customer> DeleteAsync(Guid id)
        {
            var existingCustomer = await _db.Customer.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCustomer == null)
            {
                return null;
            }
            _db.Customer.Remove(existingCustomer);
            await _db.SaveChangesAsync();
            return existingCustomer;
        }

    
    }
}
