using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;

namespace Chinook.Domain.Entities
{
    public class InvoiceLine : IConvertModel<InvoiceLine, InvoiceLineApiModel>
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int InvoiceLineId { get; set; }

        public int InvoiceId { get; set; }

        public int TrackId { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public Invoice Invoice { get; set; }
        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public Track Track { get; set; }

        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public InvoiceLineApiModel Convert => new InvoiceLineApiModel
        {
            InvoiceLineId = InvoiceLineId,
            InvoiceId = InvoiceId,
            TrackId = TrackId,
            UnitPrice = UnitPrice,
            Quantity = Quantity
        };
    }
}