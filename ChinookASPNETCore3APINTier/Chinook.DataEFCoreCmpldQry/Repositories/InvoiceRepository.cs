using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chinook.DataEFCoreCmpldQry.Repositories
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

        private bool InvoiceExists(int id) =>
            _context.Invoice.Any(i => i.InvoiceId == id);

        public void Dispose() => _context.Dispose();

        public List<Invoice> GetAll() 
            => _context.GetAllInvoices();

        public Invoice GetById(int id)
        {
            var invoice = _context.GetInvoice(id);
            return invoice.First();
        }

        public Invoice Add(Invoice newInvoice)
        {
            _context.Invoice.Add(newInvoice);
            _context.SaveChanges();
            return newInvoice;
        }

        public bool Update(Invoice invoice)
        {
            if (!InvoiceExists(invoice.InvoiceId))
                return false;
            _context.Invoice.Update(invoice);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!InvoiceExists(id))
                return false;
            var toRemove = _context.Invoice.Find(id);
            _context.Invoice.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }

        public List<Invoice> GetByCustomerId(int id) 
            => _context.GetInvoicesByCustomerId(id);
    }
}