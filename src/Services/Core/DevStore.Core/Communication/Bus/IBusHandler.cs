using System.Threading.Tasks;

using DevStore.Core.Messages;
using DevStore.Core.Messages.CommonMessages.DomainEvents;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;
using DevStore.Core.Messages.CommonMessages.Notifications;

namespace DevStore.Core.Communication.Bus
{
    public interface IBusHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;

        Task PublishIntegrationEvent<T>(T @event) where T : IntegrationEvent;

        Task SendCommand<T>(T command) where T : Command;

        Task PublishNotification<T>(T @event) where T : DomainNotification;

        Task PublishDomainEvent<T>(T @event) where T : DomainEvent;
    }
}