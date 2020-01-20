using Chinook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chinook.Domain.Repositories
{
    public interface IInvoiceLineRepository : IDisposable
    {
        public bool InvoiceLineExists(int id);
        Task<IAsyncEnumerable<InvoiceLine>> GetAll();
        Task<InvoiceLine> GetById(int id);
        Task<IAsyncEnumerable<InvoiceLine>> GetByInvoiceId(int id);
        Task<IAsyncEnumerable<InvoiceLine>> GetByTrackId(int id);
        Task<InvoiceLine> Add(InvoiceLine newInvoiceLine);
        Task<bool> Update(InvoiceLine invoiceLine);
        Task<bool> Delete(int id);
    }
}