using Chinook.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Chinook.Domain.Repositories
{
    public interface IInvoiceRepository : IDisposable
    {
        List<Invoice> GetAll();
        Invoice GetById(int id);
        List<Invoice> GetByCustomerId(int id);
        Invoice Add(Invoice newInvoice);
        bool Update(Invoice invoice);
        bool Delete(int id);
        List<Invoice> GetByEmployeeId(int id);
    }
}