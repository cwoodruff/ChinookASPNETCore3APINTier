using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCore.Repositories
{
    public class InvoiceLineRepository : IInvoiceLineRepository
    {
        private readonly ChinookContext _context;

        public InvoiceLineRepository(ChinookContext context)
        {
            _context = context;
        }

        private bool InvoiceLineExists(int id) =>
            _context.InvoiceLine.Any(i => i.InvoiceLineId == id);

        public void Dispose() => _context.Dispose();

        public List<InvoiceLine> GetAll() =>
            _context.InvoiceLine.AsNoTracking().ToList();

        public InvoiceLine GetById(int id) =>
            _context.InvoiceLine.Find(id);

        public InvoiceLine Add(InvoiceLine newInvoiceLine)
        {
            _context.InvoiceLine.Add(newInvoiceLine);
            _context.SaveChanges();
            return newInvoiceLine;
        }

        public bool Update(InvoiceLine invoiceLine)
        {
            if (!InvoiceLineExists(invoiceLine.InvoiceLineId))
                return false;
            _context.InvoiceLine.Update(invoiceLine);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!InvoiceLineExists(id))
                return false;
            var toRemove = _context.InvoiceLine.Find(id);
            _context.InvoiceLine.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }

        public List<InvoiceLine> GetByInvoiceId(int id) => _context.InvoiceLine.Where(a => a.InvoiceId == id).ToList();

        public List<InvoiceLine> GetByTrackId(int id) => _context.InvoiceLine.Where(a => a.TrackId == id).ToList();
    }
}