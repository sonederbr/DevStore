using System;

namespace DevStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderEnrolledRejectedEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }

        public OrderEnrolledRejectedEvent(Guid orderId, Guid clientId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
        }
    }
}
