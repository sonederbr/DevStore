using System;

using DevStore.Core.DomainObjects.DTO;

namespace DevStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderStartedEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public decimal Total { get; private set; }
        public CoursesOrderDto CoursesOrder { get; private set; }
        public string NameCard { get; private set; }
        public string NumberCard { get; private set; }
        public string ExpirationDateCard { get; private set; }
        public string CvvCard { get; private set; }

        public OrderStartedEvent(Guid orderId, Guid clientId, decimal total, CoursesOrderDto items, string nameCard, string numberCard, string expirationDateCard, string cvvCard)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
            Total = total;
            CoursesOrder = items;
            NameCard = nameCard;
            NumberCard = numberCard;
            ExpirationDateCard = expirationDateCard;
            CvvCard = cvvCard;
        }
    }
}
