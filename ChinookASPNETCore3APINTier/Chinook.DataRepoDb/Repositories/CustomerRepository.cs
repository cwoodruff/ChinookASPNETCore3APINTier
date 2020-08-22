using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using RepoDb;

namespace Chinook.DataRepoDb.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbInfo _dbInfo;

        public CustomerRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
            RepoDb.SqlServerBootstrap.Initialize();
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);
        
        public void Dispose()
        {
            
        }

        private bool CustomerExists(int id) => 
            Connection.Exists("select count(1) from Customer where CustomerId = @id", new {id});

        public List<Customer> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var customers = cn.QueryAll<Customer>();
            return customers.ToList();
        }

        public Customer GetById(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.Query<Customer>(id).FirstOrDefault();
        }

        public List<Customer> GetBySupportRepId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var customers = cn.Query<Customer>(c => c.SupportRepId == id);
            return customers.ToList();
        }

        public Customer Add(Customer newCustomer)
        {
            using var cn = Connection;
            cn.Open();
            cn.Insert(newCustomer);
            return newCustomer;
        }

        public bool Update(Customer customer)
        {
            if (!CustomerExists(customer.CustomerId))
                return false;

            try
            {
                using var cn = Connection;
                cn.Open();
                return (cn.Update(customer) > 0);
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using var cn = Connection;
                cn.Open();
                return (cn.Delete(new Customer {CustomerId = id}) > 0);
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}