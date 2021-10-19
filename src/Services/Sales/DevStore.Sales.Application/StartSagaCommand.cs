using System;

using DevStore.Core.Messages;

namespace DevStore.Sales.Application
{
    public class StartSagaCommand : Command
    {
        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public decimal Total { get; private set; }
        public string NameCard { get; private set; }
        public string NumberCard { get; private set; }
        public string ExpirationDateCard { get; private set; }
        public string CvvCard { get; private set; }

        public StartSagaCommand(Guid clientId, Guid orderId, decimal total, string nameCard, string numberCard, string expirationDateCard, string cvvCard)
        {
            AggregateId = orderId;
            ClientId = clientId;
            OrderId = orderId;
            Total = total;
            NameCard = nameCard;
            NumberCard = numberCard;
            ExpirationDateCard = expirationDateCard;
            CvvCard = cvvCard;
        }
    }
}