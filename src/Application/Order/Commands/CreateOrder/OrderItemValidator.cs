using Application.Common.Models;
using FluentValidation;

namespace Application.Order.Commands.CreateOrder;

public class OrderItemValidator : AbstractValidator<OrderItem>
{
    public OrderItemValidator()
    {
        RuleFor(x => x.Quantity).GreaterThan(0)
            .WithMessage("The Quantity must be greater than zero");
        RuleFor(x => x.ProductType).NotEmpty()
            .WithMessage("Product Type  can't empty");
    }
}