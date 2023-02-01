using Application.Common.Exceptions;
using Application.Repositories;
using Ardalis.GuardClauses;
using MediatR;
using NotFoundException = Application.Common.Exceptions.NotFoundException;

namespace Application.Order.Commands.CancelOrder;

public record CancelOrderCommand : IRequest<bool>
{
    public string OrderNumber { get; set; }
}

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, bool>
{
    private readonly IOrderRepository orderRepository;

    public CancelOrderCommandHandler(IOrderRepository orderRepository)
    {
        this.orderRepository=Guard.Against.Null(orderRepository, nameof(IOrderRepository));
    }
    public async Task<bool> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
    {

        var order = await orderRepository.GetOrderWithItemAsync(command.OrderNumber);

        if (order== null)
        {
            throw new NotFoundException("Order not found!");
        }

        if (order.Status == Domain.Enums.OrderStatus.Canceled)
        {
            throw new ValidationException("Order already has cancel status", nameof(Domain.Enums.OrderStatus));
        }

        if (order.Status != Domain.Enums.OrderStatus.New)
        {
            throw new ValidationException("You can cancel New orders", nameof(Domain.Enums.OrderStatus));
        }

        order.CancelOrder();

        await orderRepository.UpdateAsync(order);

        return true;
    }
}
