using System;
using System.Collections.Generic;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataJson.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbInfo _dbInfo;

        public CustomerRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        public void Dispose()
        {
        }

        private bool CustomerExists(int id)
        {
            return true;
        }

        public List<Customer> GetAll()
        {
            return null;
        }

        public Customer GetById(int id)
        {
            return null;
        }

        public List<Customer> GetBySupportRepId(int id)
        {
            return null;
        }

        public Customer Add(Customer newCustomer)
        {
            return newCustomer;
        }

        public bool Update(Customer customer)
        {
            if (!CustomerExists(customer.CustomerId))
                return false;

            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}