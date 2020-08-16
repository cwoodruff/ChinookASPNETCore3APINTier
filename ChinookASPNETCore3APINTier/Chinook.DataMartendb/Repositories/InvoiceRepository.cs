using System;
using System.Collections.Generic;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;

namespace Chinook.DataMartendb.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<Invoice> GetAll()
        {
            throw new NotImplementedException();
        }

        public Invoice GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Invoice> GetByCustomerId(int id)
        {
            throw new NotImplementedException();
        }

        public Invoice Add(Invoice newInvoice)
        {
            throw new NotImplementedException();
        }

        public bool Update(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Invoice> GetByEmployeeId(int id)
        {
            throw new NotImplementedException();
        }
    }
}