using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.MockData.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void Dispose()
        {
        }

        public bool CustomerExists(int id) => true;

        public async Task<IAsyncEnumerable<Customer>> GetAll()
            => (new List<Customer>
            {new Customer
            {
                CustomerId = 1
            }}).ToAsyncEnumerable();

        public async Task<Customer> GetById(int id)
            => new Customer
            {
                CustomerId = id
            };

        public async Task<Customer> Add(Customer newCustomer) => newCustomer;

        public async Task<bool> Update(Customer customer) => true;

        public async Task<bool> Delete(int id) => true;

        public async Task<IAsyncEnumerable<Customer>> GetBySupportRepId(int id)
            => (new List<Customer>
            {new Customer
            {
                CustomerId = id
            }}).ToAsyncEnumerable();
    }
}