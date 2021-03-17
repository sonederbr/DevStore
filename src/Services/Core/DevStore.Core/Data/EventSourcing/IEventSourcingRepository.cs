using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevStore.Core.Messages;

namespace DevStore.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        Task SaveEvent<TEvent>(TEvent @event) where TEvent : Event;
        Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId);
    }
}