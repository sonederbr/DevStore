using System.Threading.Tasks;

using DevStore.Core.Messages;
using DevStore.Core.Messages.CommonMessages.DomainEvents;
using DevStore.Core.Messages.CommonMessages.Notifications;

namespace DevStore.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;

        Task<bool> SendCommand<T>(T command) where T : Command;

        Task PublishNotification<T>(T @event) where T : DomainNotification;

        Task PublishDomainEvent<T>(T @event) where T : DomainEvent;
    }
}