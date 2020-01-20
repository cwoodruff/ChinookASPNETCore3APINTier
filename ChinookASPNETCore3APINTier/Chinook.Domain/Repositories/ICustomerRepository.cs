using Chinook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chinook.Domain.Repositories
{
    public interface ICustomerRepository : IDisposable
    {
        bool CustomerExists(int id);
        Task<IAsyncEnumerable<Customer>> GetAll();
        Task<Customer> GetById(int id);
        Task<IAsyncEnumerable<Customer>> GetBySupportRepId(int id);
        Task<Customer> Add(Customer newCustomer);
        Task<bool> Update(Customer customer);
        Task<bool> Delete(int id);
    }
}