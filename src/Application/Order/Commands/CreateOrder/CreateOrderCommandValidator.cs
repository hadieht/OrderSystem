using Application.Common.Models;
using FluentValidation;

namespace Application.Order.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {

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

            RuleFor(x => x.Items)
                .Must(x => x != null && x.Any())
                .WithMessage("Order should have at least one item!");

            RuleForEach(a => a.Items).SetValidator(new OrderItemValidator());
        }

        private bool AddressIsValid(Address address)
        {
            return address.PostalCode != null &&
                 address.HouseNumber > 0;
        }

    }
}
