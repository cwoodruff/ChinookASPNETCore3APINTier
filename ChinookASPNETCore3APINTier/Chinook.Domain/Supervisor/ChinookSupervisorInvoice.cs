using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public bool InvoiceExists(int id) => _invoiceRepository.InvoiceExists(id);
        public async Task<IAsyncEnumerable<InvoiceApiModel>> GetAllInvoice()
        {
            var invoices = (await _invoiceRepository.GetAll()).ConvertAll();
            await foreach (var invoice in invoices)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Invoice-", invoice.InvoiceId), invoice, cacheEntryOptions);
            }
            return invoices;
        }
        
        public async Task<InvoiceApiModel> GetInvoiceById(int id)
        {
            var invoiceApiModelCached = _cache.Get<InvoiceApiModel>(string.Concat("Invoice-", id));

            if (invoiceApiModelCached != null)
            {
                return invoiceApiModelCached;
            }
            else
            {
                var invoiceApiModel = (await _invoiceRepository.GetById(id)).Convert();
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Invoice-", invoiceApiModel.InvoiceId), invoiceApiModel, cacheEntryOptions);

                return invoiceApiModel;
            }
        }

        public async Task<IAsyncEnumerable<InvoiceApiModel>> GetInvoiceByCustomerId(int id) => (await _invoiceRepository.GetByCustomerId(id)).ConvertAll();

        public async Task<InvoiceApiModel> AddInvoice(InvoiceApiModel newInvoiceApiModel)
        {
            var invoice = newInvoiceApiModel.Convert();
            invoice = await _invoiceRepository.Add(invoice);
            return invoice.Convert();
        }

        public async Task<bool> UpdateInvoice(InvoiceApiModel invoiceApiModel) => await _invoiceRepository.Update(invoiceApiModel.Convert());

        public async Task<bool> DeleteInvoice(int id) => await _invoiceRepository.Delete(id);
    }
}