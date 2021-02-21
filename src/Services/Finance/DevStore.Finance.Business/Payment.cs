using System;

using DevStore.Core.Messages.CommonMessages.DomainEvents;

namespace DevStore.Finance.Business
{
    public class Payment : Entity, IAggregateRoot
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }

        public string NameCard { get; set; }
        public string NumberCard { get; set; }
        public string ExpirationDateCard { get; set; }
        public string CvvCard { get; set; }

        // EF. Rel.
        public Transaction Transaction { get; set; }
    }
}
