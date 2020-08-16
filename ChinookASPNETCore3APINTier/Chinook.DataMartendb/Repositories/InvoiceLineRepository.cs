using System;
using System.Collections.Generic;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;

namespace Chinook.DataMartendb.Repositories
{
    public class InvoiceLineRepository : IInvoiceLineRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<InvoiceLine> GetAll()
        {
            throw new NotImplementedException();
        }

        public InvoiceLine GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<InvoiceLine> GetByInvoiceId(int id)
        {
            throw new NotImplementedException();
        }

        public List<InvoiceLine> GetByTrackId(int id)
        {
            throw new NotImplementedException();
        }

        public InvoiceLine Add(InvoiceLine newInvoiceLine)
        {
            throw new NotImplementedException();
        }

        public bool Update(InvoiceLine invoiceLine)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}