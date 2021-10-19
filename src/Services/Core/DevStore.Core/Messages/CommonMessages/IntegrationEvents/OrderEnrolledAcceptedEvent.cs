using System;
using System.Collections.Generic;

using DevStore.Core.DomainObjects.DTO;

namespace DevStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderEnrolledAcceptedEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public decimal Total { get; private set; }
        public string NameCard { get; private set; }
        public string NumberCard { get; private set; }
        public string ExpirationDateCard { get; private set; }
        public string CvvCard { get; private set; }
        public ICollection<Guid> CourseIds { get; private set; }

        public OrderEnrolledAcceptedEvent(Guid orderId, Guid clientId, decimal total, string nameCard, string numberCard, string expirationDateCard, string cvvCard, ICollection<Guid> courseIds)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
            Total = total;
            NameCard = nameCard;
            NumberCard = numberCard;
            ExpirationDateCard = expirationDateCard;
            CvvCard = cvvCard;
            CourseIds = courseIds;
        }
    }
}
