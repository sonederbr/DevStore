using System;
using System.Collections.Generic;

namespace DevStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderCanceledEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public ICollection<Guid> CourseIds { get; private set; }

        public OrderCanceledEvent(Guid orderId, Guid clientId, ICollection<Guid> courseIds)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
            CourseIds = courseIds;
        }
    }
}
