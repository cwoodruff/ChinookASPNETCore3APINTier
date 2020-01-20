using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.Data.Repositories
{
    public class InvoiceLineRepository : IInvoiceLineRepository
    {
        private readonly ChinookContext _context;

        public InvoiceLineRepository(ChinookContext context)
        {
            _context = context;
        }

        public bool InvoiceLineExists(int id) => _context.InvoiceLine.Any(i => i.InvoiceLineId == id);

        public void Dispose() => _context.Dispose();

        public async Task<IAsyncEnumerable<InvoiceLine>> GetAll() => await _context.GetAllInvoiceLines();

        public async Task<InvoiceLine> GetById(int id) => await _context.GetInvoiceLine(id);

        public async Task<InvoiceLine> Add(InvoiceLine newInvoiceLine)
        {
            if (newInvoiceLine == null)
                return null;
            var invoiceLine = await _context.InvoiceLine.AddAsync(newInvoiceLine);
            await _context.SaveChangesAsync();
            return invoiceLine.Entity;
        }

        public async Task<bool> Update(InvoiceLine invoiceLine)
        {
            if (!InvoiceLineExists(invoiceLine.InvoiceLineId))
                return false;
            _context.InvoiceLine.Update(invoiceLine);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!InvoiceLineExists(id))
                return false;
            var toRemove = _context.InvoiceLine.Find(id);
            _context.InvoiceLine.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IAsyncEnumerable<InvoiceLine>> GetByInvoiceId(int id) => await _context.GetInvoiceLinesByInvoiceId(id);

        public async Task<IAsyncEnumerable<InvoiceLine>> GetByTrackId(int id) => await _context.GetInvoiceLinesByTrackId(id);
    }
}