using System.Threading.Tasks;

using DevStore.Core.Data.EventSourcing;
using DevStore.Core.Messages;
using DevStore.Core.Messages.CommonMessages.DomainEvents;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;
using DevStore.Core.Messages.CommonMessages.Notifications;

using MediatR;

using Rebus.Bus;

namespace DevStore.Core.Communication.Bus
{
    public class BusHandler : IBusHandler
    {
        private readonly IBus _bus;
        private readonly IEventSourcingRepository _eventSourcingRepository;

        public BusHandler(IBus bus,
                               IEventSourcingRepository eventSourcingRepository)
        {
            _bus = bus;
            _eventSourcingRepository = eventSourcingRepository;
        }

        public async Task PublishEvent<T>(T @event) where T : Event
        {
            await _bus.Publish(@event);
            await _eventSourcingRepository.SaveEvent(@event);
        }

        public async Task PublishIntegrationEvent<T>(T @event) where T : IntegrationEvent
        {
            await _bus.Publish(@event);
            await _eventSourcingRepository.SaveIntegrationEvent(@event);
        }

        public async Task SendCommand<T>(T command) where T : Command
        {
            await _bus.Send(command);
        }

        public async Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            await _bus.Publish(notification);
        }

        public async Task PublishDomainEvent<T>(T @event) where T : DomainEvent
        {
            await _bus.Publish(@event);
        }
    }
}