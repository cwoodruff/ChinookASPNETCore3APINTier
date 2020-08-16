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
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DbInfo _dbInfo;

        public InvoiceRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);

        public void Dispose()
        {
            
        }

        private bool InvoiceExists(int id) =>
            Connection.ExecuteScalar<bool>("select count(1) from Invoice where InvoiceId = @id", new {id});

        public List<Invoice> GetAll()
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var invoices =  Connection.QueryAll<Invoice>();
                return invoices.ToList();
            }
        }

        public Invoice GetById(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                return cn.Query<Invoice>(i => i.InvoiceId == id).FirstOrDefault();
            }
        }

        public List<Invoice> GetByCustomerId(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                var invoices = cn.Query<Invoice>(i => i.CustomerId == id);
                return invoices.ToList();
            }
        }

        public Invoice Add(Invoice newInvoice)
        {
            using (var cn = Connection)
            {
                cn.Open();

                newInvoice.InvoiceId = (int) cn.Insert(
                    new Invoice
                    {
                        InvoiceId = newInvoice.InvoiceId,
                        CustomerId = newInvoice.CustomerId,
                        InvoiceDate = newInvoice.InvoiceDate,
                        BillingAddress = newInvoice.BillingAddress,
                        BillingCity = newInvoice.BillingCity,
                        BillingState = newInvoice.BillingState,
                        BillingCountry = newInvoice.BillingCountry,
                        BillingPostalCode = newInvoice.BillingPostalCode,
                        Total = newInvoice.Total
                    });
            }

            return newInvoice;
        }

        public bool Update(Invoice invoice)
        {
            if (!InvoiceExists(invoice.InvoiceId))
                return false;

            try
            {
                using (var cn = Connection)
                {
                    cn.Open();
                    return (cn.Update(invoice) > 0);
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
                    return cn.Delete(new Invoice {InvoiceId = id}) > 0;
                }  
            }
            catch(Exception)
            {
                return false;
            }
        }

        public List<Invoice> GetByEmployeeId(int id)
        {
            throw new NotImplementedException();
        }
    }
}