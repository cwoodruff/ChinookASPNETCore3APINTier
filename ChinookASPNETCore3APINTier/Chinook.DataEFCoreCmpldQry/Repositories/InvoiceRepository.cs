using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCoreCmpldQry.Repositories
{
    /// <summary>
    /// The invoice repository.
    /// </summary>
    public class InvoiceRepository : EfRepository<Album>
    {
        public InvoiceRepository(ChinookContext context) : base(context)
        {
        }

        public List<Invoice> GetAll() 
            => _dbContext.GetAllInvoices();

        public Invoice GetById(int id)
        {
            var invoice = _dbContext.GetInvoice(id);
            return invoice;
        }

        public Invoice Add(Invoice newInvoice)
        {
            _dbContext.Invoice.Add(newInvoice);
            _dbContext.SaveChanges();
            return newInvoice;
        }

        public bool Update(Invoice invoice)
        {
            if (!Exists(invoice.InvoiceId))
                return false;
            _dbContext.Invoice.Update(invoice);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!Exists(id))
                return false;
            var toRemove = _dbContext.Invoice.Find(id);
            _dbContext.Invoice.Remove(toRemove);
            _dbContext.SaveChanges();
            return true;
        }

        public List<Invoice> GetByEmployeeId(int id) => _dbContext.GetInvoicesByEmployeeId(id);

        public List<Invoice> GetByCustomerId(int id) 
            => _dbContext.GetInvoicesByCustomerId(id);
    }
}