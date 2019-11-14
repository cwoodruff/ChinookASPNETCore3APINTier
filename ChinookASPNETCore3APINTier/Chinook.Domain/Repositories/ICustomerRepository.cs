using Chinook.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Chinook.Domain.Repositories
{
    public interface ICustomerRepository : IDisposable
    {
        List<Customer> GetAll();
        Customer GetById(int id);
        List<Customer> GetBySupportRepId(int id);
        Customer Add(Customer newCustomer);
        bool Update(Customer customer);
        bool Delete(int id);
    }
}