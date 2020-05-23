using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCore.Repositories
{
    public class InvoiceRepository : EfRepository<Album>
    {
        public InvoiceRepository(ChinookContext context) : base(context)
        {
        }

        public List<Invoice> GetByEmployeeId(int id) =>
            _dbContext.Customer.Where(a => a.SupportRepId == 5).SelectMany(t => t.Invoices).ToList();

        public List<Invoice> GetByCustomerId(int id) =>
            _dbContext.Invoice.Where(i => i.CustomerId == id).ToList();
    }
}