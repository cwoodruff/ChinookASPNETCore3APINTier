using System;
using System.Collections.Generic;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataJson.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DbInfo _dbInfo;

        public InvoiceRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        public void Dispose()
        {
        }

        private bool InvoiceExists(int id)
        {
            return true;
        }

        public List<Invoice> GetAll()
        {
            return null;
        }

        public Invoice GetById(int id)
        {
            return null;
        }

        public List<Invoice> GetByCustomerId(int id)
        {
            return null;
        }

        public Invoice Add(Invoice newInvoice)
        {
            return newInvoice;
        }

        public bool Update(Invoice invoice)
        {
            if (!InvoiceExists(invoice.InvoiceId))
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