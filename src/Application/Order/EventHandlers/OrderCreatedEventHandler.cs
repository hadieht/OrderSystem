using Domain.Events;
using MediatR;

namespace Application.Order.EventHandlers;

public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        
        return Task.CompletedTask;
    }
}

