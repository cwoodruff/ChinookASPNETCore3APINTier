using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCoreCmpldQry.Repositories
{
    public class InvoiceLineRepository : EfRepository<Album>
    {
        public InvoiceLineRepository(ChinookContext context) : base(context)
        {
        }

        public List<InvoiceLine> GetAll() 
            => _dbContext.GetAllInvoiceLines();

        public InvoiceLine GetById(int id)
        {
            var invoiceLine = _dbContext.GetInvoiceLine(id);
            return invoiceLine;
        }

        public InvoiceLine Add(InvoiceLine newInvoiceLine)
        {
            _dbContext.InvoiceLine.Add(newInvoiceLine);
            _dbContext.SaveChanges();
            return newInvoiceLine;
        }

        public bool Update(InvoiceLine invoiceLine)
        {
            if (!Exists(invoiceLine.InvoiceLineId))
                return false;
            _dbContext.InvoiceLine.Update(invoiceLine);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!Exists(id))
                return false;
            var toRemove = _dbContext.InvoiceLine.Find(id);
            _dbContext.InvoiceLine.Remove(toRemove);
            _dbContext.SaveChanges();
            return true;
        }

        public List<InvoiceLine> GetByInvoiceId(int id) => _dbContext.GetInvoiceLinesByInvoiceId(id);

        public List<InvoiceLine> GetByTrackId(int id) => _dbContext.GetInvoiceLinesByTrackId(id);
    }
}