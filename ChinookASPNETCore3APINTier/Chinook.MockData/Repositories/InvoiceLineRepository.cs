using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.MockData.Repositories
{
    public class InvoiceLineRepository : IInvoiceLineRepository
    {
        public void Dispose()
        {
        }

        public List<InvoiceLine> GetAll()
            => new List<InvoiceLine>
            {new InvoiceLine
            {
                InvoiceLineId = 1
            }};

        public InvoiceLine GetById(int id)
            => new InvoiceLine
            {
                InvoiceLineId = id
            };

        public InvoiceLine Add(InvoiceLine newInvoiceLine) => newInvoiceLine;

        public bool Update(InvoiceLine invoiceLine) => true;

        public bool Delete(int id) => true;

        public List<InvoiceLine> GetByInvoiceId(int id)
            => new List<InvoiceLine>
            {new InvoiceLine
            {
                InvoiceLineId = id
            }};

        public List<InvoiceLine> GetByTrackId(int id)
            => new List<InvoiceLine>
            {new InvoiceLine
            {
                InvoiceLineId = id
            }};
    }
}