using System.Threading.Tasks;

using DevStore.Core.Communication.Bus;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;
using DevStore.Sales.Application.Commands;

using Rebus.Handlers;

namespace DevStore.Sales.Application.Events
{
    public class OrderEventHandler :
        IHandleMessages<OrderDraftStartedEvent>,
        IHandleMessages<OrderItemAddedEvent>,
        IHandleMessages<OrderUpdatedEvent>,
        IHandleMessages<OrderItemRemovedEvent>,
        IHandleMessages<OrderEmptyRemovedEvent>,
        IHandleMessages<OrderVoucherAppliedEvent>,
        IHandleMessages<OrderFinishedEvent>
    {

        private readonly IBusHandler _bus;

        public OrderEventHandler(IBusHandler bus)
        {
            _bus = bus;
        }

        public Task Handle(OrderDraftStartedEvent message)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderItemAddedEvent message)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderUpdatedEvent message)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderItemRemovedEvent message)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderEmptyRemovedEvent message)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderVoucherAppliedEvent message)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderFinishedEvent message)
        {
            return Task.CompletedTask;
        }
    }
}
