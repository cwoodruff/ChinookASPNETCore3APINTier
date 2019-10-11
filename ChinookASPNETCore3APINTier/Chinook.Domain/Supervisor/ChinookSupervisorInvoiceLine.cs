using System;
using System.Collections.Generic;
using System.Threading;
using Chinook.Domain.Extensions;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public IEnumerable<InvoiceLineApiModel> GetAllInvoiceLine()
        {
            var invoiceLines = _invoiceLineRepository.GetAll();
            foreach (var invoiceLine in invoiceLines)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(invoiceLine.InvoiceLineId, invoiceLine, cacheEntryOptions);
            }
            return invoiceLines.ConvertAll();
        }

        public InvoiceLineApiModel GetInvoiceLineById(int id)
        {
            var invoiceLine = _cache.Get<InvoiceLine>(id);

            if (invoiceLine != null)
            {
                var invoiceLineApiModel = invoiceLine.Convert;
                invoiceLineApiModel.Track = GetTrackById(invoiceLineApiModel.TrackId);
                invoiceLineApiModel.Invoice = GetInvoiceById(invoiceLineApiModel.InvoiceId);
                invoiceLineApiModel.TrackName = invoiceLineApiModel.Track.Name;
                return invoiceLineApiModel;
            }
            else
            {
                var invoiceLineApiModel = (_invoiceLineRepository.GetById(id)).Convert;
                invoiceLineApiModel.Track = GetTrackById(invoiceLineApiModel.TrackId);
                invoiceLineApiModel.Invoice = GetInvoiceById(invoiceLineApiModel.InvoiceId);
                invoiceLineApiModel.TrackName = invoiceLineApiModel.Track.Name;

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(invoiceLineApiModel.InvoiceLineId, invoiceLineApiModel, cacheEntryOptions);

                return invoiceLineApiModel;
            }
        }

        public IEnumerable<InvoiceLineApiModel> GetInvoiceLineByInvoiceId(int id)
        {
            var invoiceLines = _invoiceLineRepository.GetByInvoiceId(id);
            return invoiceLines.ConvertAll();
        }

        public IEnumerable<InvoiceLineApiModel> GetInvoiceLineByTrackId(int id)
        {
            var invoiceLines = _invoiceLineRepository.GetByTrackId(id);
            return invoiceLines.ConvertAll();
        }

        public InvoiceLineApiModel AddInvoiceLine(InvoiceLineApiModel newInvoiceLineApiModel)
        {
            /*var invoiceLine = new InvoiceLine
            {
                InvoiceId = newInvoiceLineApiModel.InvoiceId,
                TrackId = newInvoiceLineApiModel.TrackId,
                UnitPrice = newInvoiceLineApiModel.UnitPrice,
                Quantity = newInvoiceLineApiModel.Quantity
            };*/

            var invoiceLine = newInvoiceLineApiModel.Convert;

            invoiceLine = _invoiceLineRepository.Add(invoiceLine);
            newInvoiceLineApiModel.InvoiceLineId = invoiceLine.InvoiceLineId;
            return newInvoiceLineApiModel;
        }

        public bool UpdateInvoiceLine(InvoiceLineApiModel invoiceLineApiModel)
        {
            var invoiceLine = _invoiceLineRepository.GetById(invoiceLineApiModel.InvoiceId);

            if (invoiceLine == null) return false;
            invoiceLine.InvoiceLineId = invoiceLineApiModel.InvoiceLineId;
            invoiceLine.InvoiceId = invoiceLineApiModel.InvoiceId;
            invoiceLine.TrackId = invoiceLineApiModel.TrackId;
            invoiceLine.UnitPrice = invoiceLineApiModel.UnitPrice;
            invoiceLine.Quantity = invoiceLineApiModel.Quantity;

            return _invoiceLineRepository.Update(invoiceLine);
        }

        public bool DeleteInvoiceLine(int id) 
            => _invoiceLineRepository.Delete(id);
    }
}