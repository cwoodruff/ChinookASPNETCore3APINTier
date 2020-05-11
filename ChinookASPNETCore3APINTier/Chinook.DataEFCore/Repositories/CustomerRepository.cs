using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCore.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ChinookContext _context;

        public CustomerRepository(ChinookContext context)
        {
            _context = context;
        }

        public CustomerRepository()
        {
            var services = new ServiceCollection();
            
            var connection = String.Empty;
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                connection = "Server=.;Database=Chinook;Trusted_Connection=True;Application Name=ChinookASPNETCoreAPINTier";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                connection = "Server=localhost,1433;Database=Chinook;User=sa;Password=P@55w0rd;Trusted_Connection=False;Application Name=ChinookASPNETCoreAPINTier";
            }

            services.AddDbContextPool<ChinookContext>(options => options.UseSqlServer(connection));
            
            var serviceProvider = services.BuildServiceProvider();

            _context = serviceProvider.GetService<ChinookContext>();
        }

        private bool CustomerExists(int id) =>
            _context.Customer.Any(c => c.CustomerId == id);

        public void Dispose() => _context.Dispose();

        public List<Customer> GetAll() =>
            _context.Customer.AsNoTracking().ToList();

        public Customer GetById(int id) =>
            _context.Customer.Find(id);

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

        public List<Customer> GetBySupportRepId(int id) => _context.Customer.Where(a => a.SupportRepId == id).ToList();
    }
}