﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using DevStore.Communication.Mediator;
using DevStore.Core.DomainObjects.DTO;
using DevStore.Core.Extensions;
using DevStore.Core.Messages;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;
using DevStore.Core.Messages.CommonMessages.Notifications;
using DevStore.Sales.Application.Events;
using DevStore.Sales.Domain;

using MediatR;

namespace DevStore.Sales.Application.Commands
{
    public class OrderCommandHandler :
        IRequestHandler<AddOrderItemCommand, bool>,
        IRequestHandler<RemoveOrderItemCommand, bool>,
        IRequestHandler<ApplyVoucherOrderCommand, bool>,
        IRequestHandler<StartOrderCommand, bool>,
        IRequestHandler<FinishOrderCommand, bool>,
        IRequestHandler<CancelOrderAndDisrollFromCourseCommand, bool>,
        IRequestHandler<CancelOrderCommand, bool>

    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public OrderCommandHandler(IOrderRepository orderRepository,
                                   IMediatorHandler mediatorHandler)
        {
            _orderRepository = orderRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(AddOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!IsValid(message)) return false;

            var order = await _orderRepository.GetDraftOrderByClientId(message.ClientId);
            var orderItem = new OrderItem(message.CourseId, message.CourseName, message.Price);

            if (order == null)
            {
                order = Order.OrderFactory.NewDraftOrder(message.ClientId);
                order.AddItem(orderItem);

                _orderRepository.Add(order);

                order.AddEvent(new OrderDraftStartedEvent(order.Id, message.CourseId, message.CourseName, message.ClientId));
            }
            else
            {
                var hasOrderItem = order.HasOrderItem(orderItem);
                order.AddItem(orderItem);

                if (hasOrderItem)
                {
                    _orderRepository.UpdateItem(order.OrderItems.FirstOrDefault(p => p.CourseId == orderItem.CourseId));
                }
                else
                {
                    _orderRepository.AddItem(orderItem);
                }

                // order.AddEvent(new OrderItemAddedEvent(order.Id, message.CourseId, message.CourseName, message.ClientId));
            }

            order.AddEvent(new OrderUpdatedEvent(order.Id, message.ClientId, order.TotalValue));

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(RemoveOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!IsValid(message)) return false;

            var order = await _orderRepository.GetDraftOrderByClientId(message.ClientId);

            if (order == null)
            {
                //await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));
                return false;
            }

            var orderItem = await _orderRepository.GetItemByOrderId(order.Id, message.CourseId);

            if (orderItem != null && !order.HasOrderItem(orderItem))
            {
                //await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Item do pedido não encontrado!"));
                return false;
            }

            order.RemoveItem(orderItem);

            _orderRepository.RemoveItem(orderItem);
            _orderRepository.Update(order);

            order.AddEvent(new OrderItemRemovedEvent(order.Id, message.CourseId, orderItem.CourseName, message.ClientId));
            // order.AddEvent(new OrderUpdatedEvent(order.Id, message.ClientId, order.TotalValue));

            if (!order.HasItems())
            {
                _orderRepository.Remove(order);

                order.AddEvent(new OrderEmptyRemovedEvent(order.Id, message.ClientId));
            }

            //O change tracker está ativo e a Order está sendo atualizada uam vez q a entidade foi recuperada e alterada.
            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(ApplyVoucherOrderCommand message, CancellationToken cancellationToken)
        {
            if (!IsValid(message)) return false;

            var order = await _orderRepository.GetDraftOrderByClientId(message.ClientId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, "Pedido não encontrado!"));
                return false;
            }

            var voucher = await _orderRepository.GetVoucherByCode(message.VoucherCode);

            if (voucher == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, "Voucher não encontrado!"));
                return false;
            }

            var voucherApplied = order.ApplyVoucher(voucher);
            if (!voucherApplied.IsValid)
            {
                foreach (var error in voucherApplied.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
                }

                return false;
            }

            //Atualizar voucher como utilizado??? Aqui ou ao finalizar

            _orderRepository.Update(order);

            order.AddEvent(new OrderVoucherAppliedEvent(order.Id, message.ClientId, order.TotalValue));
            order.AddEvent(new OrderUpdatedEvent(order.Id, message.ClientId, order.TotalValue));

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(StartOrderCommand message, CancellationToken cancellationToken)
        {
            if (!IsValid(message)) return false;

            var order = await _orderRepository.GetDraftOrderByClientId(message.ClientId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, "Pedido não encontrado!"));
                return false;
            }

            order.StartOrder();

            var listOfItemDto = new List<ItemDto>();
            order.OrderItems.ForEach(p => listOfItemDto.Add(new ItemDto { CourseId = p.CourseId }));
            var coursesOrder = new CoursesOrderDto { OrderId = order.Id, Items = listOfItemDto };

            order.AddEvent(new OrderStartedEvent(order.Id, order.ClientId, order.TotalValue, coursesOrder, message.NameCard, message.NumberCard, message.ExpirationDateCard, message.CvvCard));

            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(FinishOrderCommand message, CancellationToken cancellationToken)
        {
            if (!IsValid(message)) return false;

            var order = await _orderRepository.GetById(message.OrderId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, "Pedido não encontrado!"));
                return false;
            }

            order.FinishOrder();

            order.AddEvent(new OrderFinishedEvent(order.Id));

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(CancelOrderAndDisrollFromCourseCommand message, CancellationToken cancellationToken)
        {
            if (!IsValid(message)) return false;

            var order = await _orderRepository.GetById(message.OrderId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, "Pedido não encontrado!"));
                return false;
            }

            var listOfItemDto = new List<ItemDto>();
            order.OrderItems.ForEach(p => listOfItemDto.Add(new ItemDto { CourseId = p.CourseId }));
            var coursesOrder = new CoursesOrderDto { OrderId = order.Id, Items = listOfItemDto };

            order.AddEvent(new OrderCanceledEvent(message.OrderId, message.ClientId, coursesOrder));
            order.MakeDraft();

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(CancelOrderCommand message, CancellationToken cancellationToken)
        {
            if (!IsValid(message)) return false;

            var order = await _orderRepository.GetById(message.OrderId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, "Pedido não encontrado!"));
                return false;
            }

            order.MakeDraft();

            return await _orderRepository.UnitOfWork.Commit();
        }

        private bool IsValid(Command message)
        {
            if (message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
