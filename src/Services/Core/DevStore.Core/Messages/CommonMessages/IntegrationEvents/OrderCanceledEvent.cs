using System;

using DevStore.Core.DomainObjects.DTO;

namespace DevStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderCanceledEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public CoursesOrderDto CoursesOrder { get; private set; }

        public OrderCanceledEvent(Guid orderId, Guid clientId, CoursesOrderDto items)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
            CoursesOrder = items;
        }
    }
}
