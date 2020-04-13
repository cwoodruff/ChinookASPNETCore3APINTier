using System;
using System.Collections.Generic;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataJson.Repositories
{
    public class InvoiceLineRepository : IInvoiceLineRepository
    {
        private readonly DbInfo _dbInfo;

        public InvoiceLineRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        public void Dispose()
        {
        }

        private bool InvoiceLineExists(int id)
        {
            return true;
        }

        public List<InvoiceLine> GetAll()
        {
            return null;
        }

        public InvoiceLine GetById(int id)
        {
            return null;
        }

        public List<InvoiceLine> GetByInvoiceId(int id)
        {
            return null;
        }

        public List<InvoiceLine> GetByTrackId(int id)
        {
            return null;
        }

        public InvoiceLine Add(InvoiceLine newInvoiceLine)
        {
            return newInvoiceLine;
        }

        public bool Update(InvoiceLine invoiceLine)
        {
            if (!InvoiceLineExists(invoiceLine.InvoiceLineId))
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