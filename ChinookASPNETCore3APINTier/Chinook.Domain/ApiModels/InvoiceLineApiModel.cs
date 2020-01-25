using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ApiModels
{
    public class InvoiceLineApiModel : IConvertModel<InvoiceLineApiModel, InvoiceLine>
    {
        public int InvoiceLineId { get; set; }
        public int InvoiceId { get; set; }
        public int TrackId { get; set; }
        public string TrackName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public InvoiceApiModel Invoice { get; set; }
        public TrackApiModel Track { get; set; }

        public InvoiceLine Convert() =>
            new InvoiceLine
            {
                InvoiceLineId = InvoiceLineId,
                InvoiceId = InvoiceId,
                TrackId = TrackId,
                UnitPrice = UnitPrice,
                Quantity = Quantity
            };
    }
}