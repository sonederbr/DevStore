using System;
using DevStore.Core.DomainObjects;

namespace DevStore.Catalog.Domain.Events
{
    public class LowStockCourseEvent : DomainEvent
    {
        public int QuantidadeRestante { get; private set; }

        public LowStockCourseEvent(Guid aggregateId, int quantidadeRestante) : base(aggregateId)
        {
            QuantidadeRestante = quantidadeRestante;
        }
    }
}