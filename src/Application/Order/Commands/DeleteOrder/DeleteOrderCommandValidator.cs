using FluentValidation;

namespace Application.Order.Commands.DeleteOrder
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(a => a).NotNull().NotEmpty();

            RuleFor(a => a.OrderNumber).NotNull().NotEmpty();
        }
    }
}
