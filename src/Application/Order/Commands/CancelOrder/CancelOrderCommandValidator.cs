using FluentValidation;

namespace Application.Order.Commands.CancelOrder;

public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(a => a).NotNull().NotEmpty();
        RuleFor(a => a.OrderID).NotNull().NotEmpty();
    }
}
