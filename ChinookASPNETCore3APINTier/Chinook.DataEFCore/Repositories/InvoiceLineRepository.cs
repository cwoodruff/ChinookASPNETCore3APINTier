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
    public class InvoiceLineRepository : EfRepository<Album>
    {
        public InvoiceLineRepository(ChinookContext context) : base(context)
        {
        }

        public List<InvoiceLine> GetByInvoiceId(int id) => _dbContext.InvoiceLine.Where(a => a.InvoiceId == id).ToList();

        public List<InvoiceLine> GetByTrackId(int id) => _dbContext.InvoiceLine.Where(a => a.TrackId == id).ToList();
    }
}