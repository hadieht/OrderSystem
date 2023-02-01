using Domain.Common;

namespace Domain.Events
{
    public class OrderCreatedEvent : BaseEvent
    {
        public Domain.Entities.Order Order { get; }

        public OrderCreatedEvent(Domain.Entities.Order order)
        {
            this.Order = order;
        }
    }
}
