using Chinook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chinook.Domain.Repositories
{
    public interface IInvoiceRepository : IDisposable
    {
        public bool InvoiceExists(int id);
        Task<IAsyncEnumerable<Invoice>> GetAll();
        Task<Invoice> GetById(int id);
        Task<IAsyncEnumerable<Invoice>> GetByCustomerId(int id);
        Task<Invoice> Add(Invoice newInvoice);
        Task<bool> Update(Invoice invoice);
        Task<bool> Delete(int id);
    }
}