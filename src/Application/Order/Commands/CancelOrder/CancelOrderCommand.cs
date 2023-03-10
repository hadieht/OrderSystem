using MediatR;

namespace Application.Order.Commands.CancelOrder;

public record CancelOrderCommand : IRequest<bool>
{
    public string OrderID { get; set; }
}