using Chinook.Domain.Entities;
using FluentValidation;

namespace Chinook.Domain.Validation
{
    public class InvoiceValidator : AbstractValidator<Invoice>
    {
        public InvoiceValidator() {
            RuleFor(i => i.CustomerId).NotNull();
            RuleFor(i => i.InvoiceDate).NotNull();
        }
    }
}