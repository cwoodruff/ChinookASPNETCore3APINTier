using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.MockData.Repositories
{
    public class InvoiceLineRepository : IInvoiceLineRepository
    {
        public void Dispose()
        {
        }

        public bool InvoiceLineExists(int id) => true;

        public async Task<IAsyncEnumerable<InvoiceLine>> GetAll()
            => (new List<InvoiceLine>
            {new InvoiceLine
            {
                InvoiceLineId = 1
            }}).ToAsyncEnumerable();

        public async Task<InvoiceLine> GetById(int id)
            => new InvoiceLine
            {
                InvoiceLineId = id
            };

        public async Task<InvoiceLine> Add(InvoiceLine newInvoiceLine) => newInvoiceLine;

        public async Task<bool> Update(InvoiceLine invoiceLine) => true;

        public async Task<bool> Delete(int id) => true;

        public async Task<IAsyncEnumerable<InvoiceLine>> GetByInvoiceId(int id)
            => (new List<InvoiceLine>
            {new InvoiceLine
            {
                InvoiceLineId = id
            }}).ToAsyncEnumerable();

        public async Task<IAsyncEnumerable<InvoiceLine>> GetByTrackId(int id)
            => (new List<InvoiceLine>
            {new InvoiceLine
            {
                InvoiceLineId = id
            }}).ToAsyncEnumerable();
    }
}