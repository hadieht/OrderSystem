using Domain.Common;

namespace Domain.Events
{
    public class OrderEditedEvent : BaseEvent
    {
        public Domain.Entities.Order Order { get; }

        public OrderEditedEvent(Domain.Entities.Order order)
        {
            this.Order = order;
        }
    }
}
