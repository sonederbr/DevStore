using System;

namespace DevStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PaymentRefusedEvent : IntegrationEvent
    {
        public Guid PaymentId { get; private set; }
        public Guid TransactionId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public decimal Total { get; private set; }

        public PaymentRefusedEvent(Guid paymentId, Guid transactionId, Guid orderId, Guid clientId, decimal total)
        {
            AggregateId = paymentId;
            PaymentId = paymentId;
            TransactionId = transactionId;
            OrderId = orderId;
            ClientId = clientId;
            Total = total;
        }
    }
}