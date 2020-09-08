using Chinook.Domain.Entities;
using FluentValidation;

namespace Chinook.Domain.Validation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator() {
            RuleFor(c => c.FirstName).NotNull();
            RuleFor(c => c.LastName).NotNull();
            RuleFor(c => c.Email).EmailAddress();
            RuleFor(c => c.Phone).Matches(@"\(?\d{3}\)?[-\.]? *\d{3}[-\.]? *[-\.]?\d{4}");
            RuleFor(c => c.Fax).Matches(@"\(?\d{3}\)?[-\.]? *\d{3}[-\.]? *[-\.]?\d{4}");
        }
    }
}