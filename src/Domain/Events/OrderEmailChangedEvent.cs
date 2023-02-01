using Domain.Common;

namespace Domain.Events
{
    public class OrderEmailChangedEvent : BaseEvent
    {
        public int OrderId { get; }
        public Email NewEmail { get; }

        public OrderEmailChangedEvent(int orderId, Email newEmail)
        {
            OrderId = orderId;
            NewEmail = newEmail;
        }
       
    }
}
