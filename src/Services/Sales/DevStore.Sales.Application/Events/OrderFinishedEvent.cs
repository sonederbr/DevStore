using System;

using DevStore.Core.Messages.CommonMessages.IntegrationEvents;

namespace DevStore.Sales.Application.Events
{
    public class OrderFinishedEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }

        public OrderFinishedEvent(Guid orderId)
        {
            AggregateId = orderId;
            OrderId = orderId;
        }
    }
}
