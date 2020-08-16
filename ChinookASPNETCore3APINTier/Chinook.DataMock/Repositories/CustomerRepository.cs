using System.Collections.Generic;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataMock.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void Dispose()
        {
        }

        public List<Customer> GetAll()
            => new List<Customer>
            {new Customer
            {
                CustomerId = 1
            }};

        public Customer GetById(int id)
            => new Customer
            {
                CustomerId = id
            };

        public Customer Add(Customer newCustomer) => newCustomer;

        public bool Update(Customer customer) => true;

        public bool Delete(int id) => true;

        public List<Customer> GetBySupportRepId(int id)
            => new List<Customer>
            {new Customer
            {
                CustomerId = id
            }};
    }
}