using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ChinookContext _context;

        public CustomerRepository(ChinookContext context)
        {
            _context = context;
        }

        public bool CustomerExists(int id) => _context.Customer.Any(c => c.CustomerId == id);

        public void Dispose() => _context.Dispose();

        public async Task<IAsyncEnumerable<Customer>> GetAll() => await _context.GetAllCustomers();

        public async Task<Customer> GetById(int id) => await _context.GetCustomer(id);
        
        public async Task<Customer> Add(Customer newCustomer)
        {
            if (newCustomer == null)
                return null;
            var customer = await _context.Customer.AddAsync(newCustomer);
            await _context.SaveChangesAsync();
            return customer.Entity;
        }

        public async Task<bool> Update(Customer customer)
        {
            if (!CustomerExists(customer.CustomerId))
                return false;
            _context.Customer.Update(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!CustomerExists(id))
                return false;
            var toRemove = _context.Customer.Find(id);
            _context.Customer.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<IAsyncEnumerable<Customer>> GetBySupportRepId(int id) => _context.GetCustomerBySupportRepId(id);
    }
}