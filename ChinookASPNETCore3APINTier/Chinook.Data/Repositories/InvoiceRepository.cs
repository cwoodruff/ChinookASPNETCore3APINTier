using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;

namespace Chinook.Data.Repositories
{
    /// <summary>
    /// The invoice repository.
    /// </summary>
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ChinookContext _context;
        
        public InvoiceRepository(ChinookContext context)
        {
            _context = context;
        }

        public bool InvoiceExists(int id) => _context.Invoice.Any(i => i.InvoiceId == id);

        public void Dispose() => _context.Dispose();

        public async Task<IAsyncEnumerable<Invoice>> GetAll() => await _context.GetAllInvoices();

        public async Task<Invoice> GetById(int id) => await _context.GetInvoice(id);

        public async Task<Invoice> Add(Invoice newInvoice)
        {
            if (newInvoice == null)
                return null;
            var invoice = await _context.Invoice.AddAsync(newInvoice);
            await _context.SaveChangesAsync();
            return invoice.Entity;
        }

        public async Task<bool> Update(Invoice invoice)
        {
            if (!InvoiceExists(invoice.InvoiceId))
                return false;
            _context.Invoice.Update(invoice);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!InvoiceExists(id))
                return false;
            var toRemove = _context.Invoice.Find(id);
            _context.Invoice.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IAsyncEnumerable<Invoice>> GetByCustomerId(int id) => await _context.GetInvoicesByCustomerId(id);
    }
}