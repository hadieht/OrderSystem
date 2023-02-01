using MediatR;

namespace Application.Order.Commands.DeleteOrder;

public record DeleteOrderCommand : IRequest<bool>
{
    public string OrderNumber { get; set; }
}