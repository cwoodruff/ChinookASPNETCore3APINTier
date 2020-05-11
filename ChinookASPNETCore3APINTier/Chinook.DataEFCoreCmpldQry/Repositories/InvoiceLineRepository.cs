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
    public class InvoiceLineRepository : IInvoiceLineRepository
    {
        private readonly ChinookContext _context;

        public InvoiceLineRepository(ChinookContext context)
        {
            _context = context;
        }

        public InvoiceLineRepository()
        {
            var services = new ServiceCollection();
            
            var connection = String.Empty;
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                connection = "Server=.;Database=Chinook;Trusted_Connection=True;Application Name=ChinookASPNETCoreAPINTier";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                connection = "Server=localhost,1433;Database=Chinook;User=sa;Password=P@55w0rd;Trusted_Connection=False;Application Name=ChinookASPNETCoreAPINTier";
            }

            services.AddDbContextPool<ChinookContext>(options => options.UseSqlServer(connection));
            
            var serviceProvider = services.BuildServiceProvider();

            _context = serviceProvider.GetService<ChinookContext>();
        }

        private bool InvoiceLineExists(int id) =>
            _context.InvoiceLine.Any(i => i.InvoiceLineId == id);

        public void Dispose() => _context.Dispose();

        public List<InvoiceLine> GetAll() 
            => _context.GetAllInvoiceLines();

        public InvoiceLine GetById(int id)
        {
            var invoiceLine = _context.GetInvoiceLine(id);
            return invoiceLine;
        }

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

        public List<InvoiceLine> GetByInvoiceId(int id) => _context.GetInvoiceLinesByInvoiceId(id);

        public List<InvoiceLine> GetByTrackId(int id) => _context.GetInvoiceLinesByTrackId(id);
    }
}