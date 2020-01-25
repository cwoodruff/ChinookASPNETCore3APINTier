using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chinook.DataEFCoreCmpldQry.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ChinookContext _context;

        public CustomerRepository(ChinookContext context)
        {
            _context = context;
        }

        private bool CustomerExists(int id) =>
            _context.Customer.Any(c => c.CustomerId == id);

        public void Dispose() => _context.Dispose();

        public List<Customer> GetAll() 
            => _context.GetAllCustomers();

        public Customer GetById(int id)
        {
            var customer = _context.GetCustomer(id);
            return customer;
        }

        public Customer Add(Customer newCustomer)
        {
            _context.Customer.Add(newCustomer);
            _context.SaveChanges();
            return newCustomer;
        }

        public bool Update(Customer customer)
        {
            if (!CustomerExists(customer.CustomerId))
                return false;
            _context.Customer.Update(customer);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!CustomerExists(id))
                return false;
            var toRemove = _context.Customer.Find(id);
            _context.Customer.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }

        public List<Customer> GetBySupportRepId(int id) => _context.GetCustomerBySupportRepId(id);
    }
}