using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public bool InvoiceLineExists(int id) => _invoiceLineRepository.InvoiceLineExists(id);
        public async Task<IAsyncEnumerable<InvoiceLineApiModel>> GetAllInvoiceLine()
        {
            var invoiceLines = (await _invoiceLineRepository.GetAll()).ConvertAll();
            await foreach (var invoiceLine in invoiceLines)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("InvoiceLine-", invoiceLine.InvoiceLineId), invoiceLine, cacheEntryOptions);
            }
            return invoiceLines;
        }

        public async Task<InvoiceLineApiModel> GetInvoiceLineById(int id)
        {
            var invoiceLineApiModelCached = _cache.Get<InvoiceLineApiModel>(string.Concat("InvoiceLine-", id));

            if (invoiceLineApiModelCached != null)
            {
                return invoiceLineApiModelCached;
            }
            else
            {
                var invoiceLineApiModel = (await _invoiceLineRepository.GetById(id)).Convert();
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("InvoiceLine-", invoiceLineApiModel.InvoiceLineId), invoiceLineApiModel, cacheEntryOptions);

                return invoiceLineApiModel;
            }
        }

        public async Task<IAsyncEnumerable<InvoiceLineApiModel>> GetInvoiceLineByInvoiceId(int id) => 
            (await _invoiceLineRepository.GetByInvoiceId(id)).ConvertAll();

        public async Task<IAsyncEnumerable<InvoiceLineApiModel>> GetInvoiceLineByTrackId(int id) =>
            (await _invoiceLineRepository.GetByTrackId(id)).ConvertAll();

        public async Task<InvoiceLineApiModel> AddInvoiceLine(InvoiceLineApiModel newInvoiceLineApiModel)
        {
            var invoiceLine = newInvoiceLineApiModel.Convert();
            invoiceLine = await _invoiceLineRepository.Add(invoiceLine);
            return invoiceLine.Convert();
        }

        public async Task<bool> UpdateInvoiceLine(InvoiceLineApiModel invoiceLineApiModel) => 
            await _invoiceLineRepository.Update(invoiceLineApiModel.Convert());

        public async Task<bool> DeleteInvoiceLine(int id) => await _invoiceLineRepository.Delete(id);
    }
}