using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Chinook.DataJson.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SqlConnection _sqlconn;

        public CustomerRepository(SqlConnection sqlconn)
        {
            _sqlconn = sqlconn;
        }

        public void Dispose()
        {
        }

        private bool CustomerExists(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_CheckCustomer", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("CustomerId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);

            return Convert.ToBoolean(dset.Tables[0].Rows[0][0]);
        }

        public List<Customer> GetAll()
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetCustomer", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Customer>)) as List<Customer>;
            return converted;
        }

        public Customer GetById(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetCustomerDetails", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("CustomerId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Customer>)) as List<Customer>;

            return converted.FirstOrDefault();
        }

        public List<Customer> GetBySupportRepId(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetCustomerBySupportRep", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("SupportRepId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Customer>)) as List<Customer>;
            return converted;
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