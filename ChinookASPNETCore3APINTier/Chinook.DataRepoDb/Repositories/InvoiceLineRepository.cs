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
            Connection.Exists("select count(1) from InvoiceLine where InvoiceLineId = @id", new {id});

        public List<InvoiceLine> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var invoiceLines = Connection.QueryAll<InvoiceLine>();
            return invoiceLines.ToList();
        }

        public InvoiceLine GetById(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.Query<InvoiceLine>(id).FirstOrDefault();
        }

        public List<InvoiceLine> GetByInvoiceId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var invoiceLines = cn.Query<InvoiceLine>(i => i.InvoiceId == id);
            return invoiceLines.ToList();
        }

        public List<InvoiceLine> GetByTrackId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var invoiceLines =  cn.Query<InvoiceLine>(i => i.TrackId == id);
            return invoiceLines.ToList();
        }

        public InvoiceLine Add(InvoiceLine newInvoiceLine)
        {
            using var cn = Connection;
            cn.Open();
            cn.Insert(newInvoiceLine);
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
                return (cn.Update(invoiceLine) > 0);
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
                return cn.Delete(new InvoiceLine {InvoiceLineId = id}) > 0;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}