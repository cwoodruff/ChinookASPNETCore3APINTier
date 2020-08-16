﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCore.Repositories
{
    /// <summary>
    /// The invoice repository.
    /// </summary>
    public class InvoiceRepository : IInvoiceRepository
    {
        /// <summary>
        /// The _context.
        /// </summary>
        private readonly ChinookContext _context;

        public InvoiceRepository(ChinookContext context)
        {
            _context = context;
        }

        private bool InvoiceExists(int id) =>
            _context.Invoice.Any(i => i.InvoiceId == id);

        public void Dispose() => _context.Dispose();

        public List<Invoice> GetAll() =>
            _context.Invoice.AsNoTracking().ToList();

        public Invoice GetById(int id) =>
            _context.Invoice.Find(id);

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

        public List<Invoice> GetByEmployeeId(int id) =>
            _context.Customer.Where(a => a.SupportRepId == 5).SelectMany(t => t.Invoices).ToList();

        public List<Invoice> GetByCustomerId(int id) =>
            _context.Invoice.Where(i => i.CustomerId == id).ToList();
    }
}