using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Chinook.DataDapper.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbInfo _dbInfo;

        public CustomerRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);
        
        public void Dispose()
        {
            
        }

        private bool CustomerExists(int id) => 
            Connection.ExecuteScalar<bool>("select count(1) from Customer where CustomerId = @id", new {id});

        public List<Customer> GetAll()
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var customers = Connection.Query<Customer>("Select * From Customer");
                return customers.ToList();
            }
        }

        public Customer GetById(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                return cn.QueryFirstOrDefault<Customer>("Select * From Customer WHERE Id = @Id", new {id});
            }
        }

        public List<Customer> GetBySupportRepId(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                var customers = cn.Query<Customer>("Select * From Customer WHERE ArtistId = @Id", new {id});
                return customers.ToList();
            }
        }

        public Customer Add(Customer newCustomer)
        {
            using (var cn = Connection)
            {
                cn.Open();

                newCustomer.CustomerId = (int) cn.Insert(
                    new Customer
                    {
                        CustomerId = newCustomer.CustomerId,
                        FirstName = newCustomer.FirstName,
                        LastName = newCustomer.LastName,
                        Company = newCustomer.Company,
                        Address = newCustomer.Address,
                        City = newCustomer.City,
                        State = newCustomer.State,
                        Country = newCustomer.Country,
                        PostalCode = newCustomer.PostalCode,
                        Phone = newCustomer.Phone,
                        Fax = newCustomer.Fax,
                        Email = newCustomer.Email,
                        SupportRepId = newCustomer.SupportRepId
                    });
            }

            return newCustomer;
        }

        public bool Update(Customer customer)
        {
            if (!CustomerExists(customer.CustomerId))
                return false;

            try
            {
                using (var cn = Connection)
                {
                    cn.Open();
                    return cn.Update(customer);
                }
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
                using (var cn = Connection)
                {
                    cn.Open();
                    return cn.Delete(new Customer {CustomerId = id});
                }  
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}