using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevStore.Core.Messages;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;

namespace DevStore.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        Task SaveEvent<TEvent>(TEvent @event) where TEvent : Event;
        Task SaveIntegrationEvent<TEvent>(TEvent @event) where TEvent : IntegrationEvent;
        Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId);
    }
}