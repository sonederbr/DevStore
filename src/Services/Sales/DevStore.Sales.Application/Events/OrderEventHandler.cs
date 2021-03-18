using System.Threading;
using System.Threading.Tasks;

using DevStore.Communication.Mediator;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;
using DevStore.Sales.Application.Commands;

using MediatR;

namespace DevStore.Sales.Application.Events
{
    public class OrderEventHandler :
        INotificationHandler<OrderDraftStartedEvent>,
        INotificationHandler<OrderItemAddedEvent>,
        INotificationHandler<OrderUpdatedEvent>,
        INotificationHandler<OrderItemRemovedEvent>,
        INotificationHandler<OrderEmptyRemovedEvent>,
        INotificationHandler<OrderVoucherAppliedEvent>,
        INotificationHandler<OrderEnrolledRejectedEvent>,
        INotificationHandler<PaymentRealizedEvent>,
        INotificationHandler<PaymentRefusedEvent>,
        INotificationHandler<OrderFinishedEvent>
    {

        private readonly IMediatorHandler _mediatorHandler;

        public OrderEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task Handle(OrderDraftStartedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderItemAddedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderUpdatedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderItemRemovedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderEmptyRemovedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderVoucherAppliedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(OrderEnrolledRejectedEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.SendCommand(new CancelOrderCommand(message.OrderId, message.ClientId));
        }

        public async Task Handle(PaymentRealizedEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.SendCommand(new FinishOrderCommand(message.OrderId, message.ClientId));
        }

        public async Task Handle(PaymentRefusedEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.SendCommand(new CancelOrderAndDisrollFromCourseCommand(message.OrderId, message.ClientId));
        }

        public Task Handle(OrderFinishedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
