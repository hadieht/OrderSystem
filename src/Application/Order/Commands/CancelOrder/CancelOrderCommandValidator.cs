using FluentValidation;

namespace Application.Order.Commands.CancelOrder;

public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(a => a.OrderNumber).NotNull().NotEmpty();
    }
}
