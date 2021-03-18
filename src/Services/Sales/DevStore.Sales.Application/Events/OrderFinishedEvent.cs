using System;

using DevStore.Core.Messages;

namespace DevStore.Sales.Application.Events
{
    public class OrderFinishedEvent : Event
    {
        public Guid OrderId { get; private set; }

        public OrderFinishedEvent(Guid orderId)
        {
            AggregateId = orderId;
            OrderId = orderId;
        }
    }
}
