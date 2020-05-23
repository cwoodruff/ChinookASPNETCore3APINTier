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
                var invoices =  Connection.Query<Invoice>("Select * From Invoice");
                return invoices.ToList();
            }
        }

        public Invoice GetById(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                return cn.QueryFirstOrDefault<Invoice>("Select * From Invoice WHERE Id = @Id", new {id});
            }
        }

        public List<Invoice> GetByCustomerId(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                var invoices = cn.Query<Invoice>("Select * From Invoice WHERE ArtistId = @Id", new { id });
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
                    return cn.Update(invoice);
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
                    return cn.Delete(new Invoice {InvoiceId = id});
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