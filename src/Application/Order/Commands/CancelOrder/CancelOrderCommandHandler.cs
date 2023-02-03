using Application.Common.Exceptions;
using Application.Repositories;
using Ardalis.GuardClauses;
using MediatR;
using NotFoundException = Application.Common.Exceptions.NotFoundException;

namespace Application.Order.Commands.CancelOrder;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, bool>
{
    private readonly IOrderRepository orderRepository;

    public CancelOrderCommandHandler(IOrderRepository orderRepository)
    {
        this.orderRepository=Guard.Against.Null(orderRepository, nameof(IOrderRepository));
    }
    public async Task<bool> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
    {

        var order = await orderRepository.GetOrderWithItemAsync(command.OrderID);

        ValidateOrder(order);

        order.CancelOrder();

        await orderRepository.UpdateAsync(order, cancellationToken);

        return true;
    }

    private void ValidateOrder(Domain.Entities.Order order)
    {
        if (order == null)
        {
            throw new NotFoundException("Order not found!");
        }

        if (order.Status == Domain.Enums.OrderStatus.Canceled)
        {
            throw new ValidationException("Order already has cancel status");
        }

        if (order.Status != Domain.Enums.OrderStatus.New)
        {
            throw new ValidationException("You can cancel New orders");
        }
    }
}