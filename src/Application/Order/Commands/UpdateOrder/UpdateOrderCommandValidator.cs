using Application.Common.Models;
using FluentValidation;

namespace Application.Order.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(a => a).NotNull().NotEmpty();

            RuleFor(a => a.OrderID).NotNull().NotEmpty();

            RuleFor(a => a.Email)
                .MaximumLength(200)
                .NotEmpty()
                .WithMessage("Email address is required.")
                .EmailAddress()
                .WithMessage("A valid email address is required.");

            RuleFor(a => a.CustomerName).NotEmpty();

            RuleFor(a => a.Address).NotNull().NotEmpty()
                .Must(AddressIsValid)
                 .WithMessage("Address is not correct!");
        }

        private bool AddressIsValid(Address address)
        {
            if (address == null)
            {
                return false;
            }

            return address.PostalCode != null &&
                 address.HouseNumber > 0;
        }
    }
}
