using System.Collections.Generic;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.MockData.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public void Dispose()
        {
        }

        public List<Invoice> GetAll()
            => new List<Invoice>
            {new Invoice
            {
                InvoiceId = 1
            }};

        public Invoice GetById(int id)
            => new Invoice
            {
                InvoiceId = id
            };

        public Invoice Add(Invoice newInvoice) => newInvoice;

        public bool Update(Invoice invoice) => true;

        public bool Delete(int id) => true;

        public List<Invoice> GetByCustomerId(int id)
            => new List<Invoice>
            {new Invoice
            {
                InvoiceId = 1
            }};
    }
}