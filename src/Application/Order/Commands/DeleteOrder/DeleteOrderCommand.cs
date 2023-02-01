using Application.Common.Exceptions;
using Application.Repositories;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.Order.Commands.DeleteOrder;

public record DeleteOrderCommand : IRequest<bool>
{
    public string OrderNumber { get; set; }
}

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly IOrderRepository orderRepository;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        this.orderRepository=Guard.Against.Null(orderRepository, nameof(IOrderRepository));
    }
    public async Task<bool> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {

        var order = await orderRepository.GetOrderWithItemAsync(command.OrderNumber);

        if (order== null)
        {
            throw new Common.Exceptions.NotFoundException("Order not found!");
        }

        await orderRepository.RemoveAsync(order);

        return true;
    }
}