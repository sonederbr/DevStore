using System;
using DevStore.Core.Messages.CommonMessages.DomainEvents;

namespace DevStore.Catalog.Domain.Events
{
    public class AlmostFullCourseEvent : DomainEvent
    {
        public int Vacancies { get; private set; }

        public AlmostFullCourseEvent(Guid aggregateId, int vacancies) : base(aggregateId)
        {
            Vacancies = vacancies;
        }
    }
}