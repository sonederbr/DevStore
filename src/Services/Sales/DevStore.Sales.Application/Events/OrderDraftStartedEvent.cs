using System;

using DevStore.Core.Messages;

namespace DevStore.Sales.Application.Events
{
    public class OrderDraftStartedEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CourseId { get; private set; }
        public string CourseName { get; private set; }
        public Guid ClientId { get; private set; }

        public OrderDraftStartedEvent(Guid orderId, Guid courseId, string courseName, Guid clientId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CourseId = courseId;
            CourseName = courseName;
            ClientId = clientId;
        }
    }
}
