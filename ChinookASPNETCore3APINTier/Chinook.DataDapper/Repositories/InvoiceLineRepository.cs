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
    public class InvoiceLineRepository : IInvoiceLineRepository
    {
        private readonly DbInfo _dbInfo;

        public InvoiceLineRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);

        public void Dispose()
        {
            
        }

        private bool InvoiceLineExists(int id) =>
            Connection.ExecuteScalar<bool>("select count(1) from InvoiceLine where InvoiceLineId = @id", new {id});

        public List<InvoiceLine> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var invoiceLines = Connection.Query<InvoiceLine>("Select * From InvoiceLine");
            return invoiceLines.ToList();
        }

        public InvoiceLine GetById(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.QueryFirstOrDefault<InvoiceLine>("Select * From InvoiceLine WHERE InvoiceLineId = @Id", new {id});
        }

        public List<InvoiceLine> GetByInvoiceId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var invoiceLines = cn.Query<InvoiceLine>("Select * From InvoiceLine WHERE ArtistId = @Id", new { id });
            return invoiceLines.ToList();
        }

        public List<InvoiceLine> GetByTrackId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var invoiceLines =  cn.Query<InvoiceLine>("Select * From InvoiceLine WHERE ArtistId = @Id", new { id });
            return invoiceLines.ToList();
        }

        public InvoiceLine Add(InvoiceLine newInvoiceLine)
        {
            using var cn = Connection;
            cn.Open();

            newInvoiceLine.InvoiceLineId = (int) cn.Insert(
                new InvoiceLine
                {
                    InvoiceLineId = newInvoiceLine.InvoiceLineId,
                    InvoiceId = newInvoiceLine.InvoiceId,
                    TrackId = newInvoiceLine.TrackId,
                    UnitPrice = newInvoiceLine.UnitPrice,
                    Quantity = newInvoiceLine.Quantity
                });

            return newInvoiceLine;
        }

        public bool Update(InvoiceLine invoiceLine)
        {
            if (!InvoiceLineExists(invoiceLine.InvoiceLineId))
                return false;

            try
            {
                using var cn = Connection;
                cn.Open();
                return cn.Update(invoiceLine);
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
                return cn.Delete(new InvoiceLine {InvoiceLineId = id});
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}