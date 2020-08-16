using System;
using System.Collections.Generic;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;

namespace Chinook.DataLinq2Db.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetBySupportRepId(int id)
        {
            throw new NotImplementedException();
        }

        public Customer Add(Customer newCustomer)
        {
            throw new NotImplementedException();
        }

        public bool Update(Customer customer)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}