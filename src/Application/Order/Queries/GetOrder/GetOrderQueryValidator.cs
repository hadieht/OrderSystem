using FluentValidation;

namespace Application.Order.Queries.GetOrder;

public class GetOrderQueryValidator : AbstractValidator<GetOrdersQuery>
{
    public GetOrderQueryValidator()
    {
        RuleFor(a => a).NotNull().NotEmpty();
        RuleFor(a => a.OrderID).NotNull().NotEmpty();
    }
}

