using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Chinook.Domain.Entities
{
    public class Invoice : BaseEntity, IConvertModel<Invoice, InvoiceApiModel>
    {
        public Invoice()
        {
            InvoiceLines = new HashSet<InvoiceLine>();
        }

        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPostalCode { get; set; }
        public decimal Total { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }
        [JsonIgnore]
        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }

        public InvoiceApiModel Convert() =>
            new InvoiceApiModel
            {
                InvoiceId = InvoiceId,
                CustomerId = CustomerId,
                InvoiceDate = InvoiceDate,
                BillingAddress = BillingAddress,
                BillingCity = BillingCity,
                BillingState = BillingState,
                BillingCountry = BillingCountry,
                BillingPostalCode = BillingPostalCode,
                Total = Total
            };
    }
}