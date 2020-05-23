using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCoreCmpldQry.Repositories
{
    public class CustomerRepository : EfRepository<Album>
    {
        public CustomerRepository(ChinookContext context) : base(context)
        {
        }

        public List<Customer> GetAll() 
            => _dbContext.GetAllCustomers();

        public Customer GetById(int id)
        {
            var customer = _dbContext.GetCustomer(id);
            return customer;
        }

        public Customer Add(Customer newCustomer)
        {
            _dbContext.Customer.Add(newCustomer);
            _dbContext.SaveChanges();
            return newCustomer;
        }

        public bool Update(Customer customer)
        {
            if (!Exists(customer.CustomerId))
                return false;
            _dbContext.Customer.Update(customer);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!Exists(id))
                return false;
            var toRemove = _dbContext.Customer.Find(id);
            _dbContext.Customer.Remove(toRemove);
            _dbContext.SaveChanges();
            return true;
        }

        public List<Customer> GetBySupportRepId(int id) => _dbContext.GetCustomerBySupportRepId(id);
    }
}