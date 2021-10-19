using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevStore.Core.Data.EventSourcing;
using DevStore.Core.Messages;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;

using EventStore.ClientAPI;

using Newtonsoft.Json;

namespace EventSourcing
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        private readonly IEventStoreService _eventStoreService;

        public EventSourcingRepository(IEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task SaveEvent<TEvent>(TEvent @event) where TEvent : Event
        {
            try
            {
                await _eventStoreService.GetConnection().AppendToStreamAsync(
                    @event.AggregateId.ToString(),
                    ExpectedVersion.Any,
                    FormatEvent(@event));
            }
            catch (Exception ex)
            {

            }
        }

        public async Task SaveIntegrationEvent<TEvent>(TEvent @event) where TEvent : IntegrationEvent
        {
            try
            {
                await _eventStoreService.GetConnection().AppendToStreamAsync(
                    @event.AggregateId.ToString(),
                    ExpectedVersion.Any,
                    FormatIntegrationEvent(@event));
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId)
        {
            try
            {
                var events = await _eventStoreService.GetConnection()
                    .ReadStreamEventsForwardAsync(aggregateId.ToString(), 0, 500, false);

                var listOfEvents = new List<StoredEvent>();

                foreach (var resolvedEvent in events.Events)
                {
                    var dataEncoded = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
                    var jsonData = JsonConvert.DeserializeObject<BaseEvent>(dataEncoded);

                    var @event = new StoredEvent(
                        resolvedEvent.Event.EventId,
                        resolvedEvent.Event.EventType,
                        jsonData.Timestamp,
                        dataEncoded);

                    listOfEvents.Add(@event);
                }

                return listOfEvents.OrderBy(e => e.DateAt);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static IEnumerable<EventData> FormatEvent<TEvent>(TEvent @event) where TEvent : Event
        {
            yield return new EventData(
                Guid.NewGuid(),
                @event.MessageType,
                true,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)),
                null);
        }

        private static IEnumerable<EventData> FormatIntegrationEvent<TEvent>(TEvent @event) where TEvent : IntegrationEvent
        {
            yield return new EventData(
                Guid.NewGuid(),
                @event.MessageType,
                true,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)),
                null);
        }
    }

    internal class BaseEvent
    {
        public DateTime Timestamp { get; set; }
    }
}
