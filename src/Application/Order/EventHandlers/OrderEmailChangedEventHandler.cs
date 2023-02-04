using Domain.Events;
using MediatR;

namespace Application.Order.EventHandlers;

public class OrderEmailChangedEventHandler : INotificationHandler<OrderEmailChangedEvent>
{
    public Task Handle(OrderEmailChangedEvent notification, CancellationToken cancellationToken)
    {

        // notify customer for changing email e.g send confirmation email
        return Task.CompletedTask;
    }
}

