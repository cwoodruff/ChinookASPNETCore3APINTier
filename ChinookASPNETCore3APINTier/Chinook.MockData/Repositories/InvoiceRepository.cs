using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.MockData.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public void Dispose()
        {
        }

        public bool InvoiceExists(int id) => true;

        public async Task<IAsyncEnumerable<Invoice>> GetAll()
            => (new List<Invoice>
            {new Invoice
            {
                InvoiceId = 1
            }}).ToAsyncEnumerable();

        public async Task<Invoice> GetById(int id)
            => new Invoice
            {
                InvoiceId = id
            };

        public async Task<Invoice> Add(Invoice newInvoice) => newInvoice;

        public async Task<bool> Update(Invoice invoice) => true;

        public async Task<bool> Delete(int id) => true;

        public async Task<IAsyncEnumerable<Invoice>> GetByCustomerId(int id)
            => (new List<Invoice>
            {new Invoice
            {
                InvoiceId = 1
            }}).ToAsyncEnumerable();
    }
}