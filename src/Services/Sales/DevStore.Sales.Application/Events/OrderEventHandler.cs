﻿using System.Threading;
using System.Threading.Tasks;

using DevStore.Core.Messages.CommonMessages.IntegrationEvents;

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
        INotificationHandler<OrderEnrolledRejectedEvent>
    {
        public Task Handle(OrderDraftStartedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderItemAddedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderItemRemovedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderEmptyRemovedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderVoucherAppliedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderEnrolledRejectedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
