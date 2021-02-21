using System;

using DevStore.Core.Messages.CommonMessages.DomainEvents;

namespace DevStore.Finance.Business
{
    public class Transaction : Entity
    {
        public Guid OrderId { get; set; }
        public Guid PaymentId { get; set; }
        public decimal Total { get; set; }
        public StatusTransaction StatusTransaction { get; set; }

        // EF. Rel.
        public Payment Payment { get; set; }
    }
}