using Domain.Events;
using MediatR;

namespace Application.Order.EventHandlers;

public class OrderEditedEventHandler : INotificationHandler<OrderEditedEvent>
{
    public Task Handle(OrderEditedEvent notification, CancellationToken cancellationToken)
    {
        // send
        return Task.CompletedTask;
    }
}

