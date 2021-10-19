using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DevStore.Core.Communication.Bus;
using DevStore.Core.DomainObjects.DTO;
using DevStore.Core.Extensions;
using DevStore.Core.Messages;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;
using DevStore.Core.Messages.CommonMessages.Notifications;
using DevStore.Sales.Application.Events;
using DevStore.Sales.Domain;

using Rebus.Handlers;

namespace DevStore.Sales.Application.Commands
{
    public class OrderCommandHandler :
        IHandleMessages<AddOrderItemCommand>,
        IHandleMessages<RemoveOrderItemCommand>,
        IHandleMessages<ApplyVoucherOrderCommand>,
        IHandleMessages<StartOrderCommand>,
        IHandleMessages<FinishOrderCommand>,
        IHandleMessages<CancelOrderAndDisrollFromCourseCommand>,
        IHandleMessages<CancelOrderCommand>

    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBusHandler _bus;

        public OrderCommandHandler(IOrderRepository orderRepository,
                                   IBusHandler bus)
        {
            _orderRepository = orderRepository;
            _bus = bus;
        }

        public async Task Handle(AddOrderItemCommand message)
        {
            if (!IsValid(message)) return;

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

            await _orderRepository.UnitOfWork.Commit();
        }

        public async Task Handle(RemoveOrderItemCommand message)
        {
            if (!IsValid(message)) return;

            var order = await _orderRepository.GetDraftOrderByClientId(message.ClientId);

            if (order == null)
            {
                //await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));
                return;
            }

            var orderItem = await _orderRepository.GetItemByOrderId(order.Id, message.CourseId);

            if (orderItem != null && !order.HasOrderItem(orderItem))
            {
                //await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Item do pedido não encontrado!"));
                return;
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
            await _orderRepository.UnitOfWork.Commit();
        }

        public async Task Handle(ApplyVoucherOrderCommand message)
        {
            if (!IsValid(message)) return;

            var order = await _orderRepository.GetDraftOrderByClientId(message.ClientId);

            if (order == null)
            {
                await _bus.PublishNotification(new DomainNotification(message.MessageType, "Pedido não encontrado!"));
                return;
            }

            var voucher = await _orderRepository.GetVoucherByCode(message.VoucherCode);

            if (voucher == null)
            {
                await _bus.PublishNotification(new DomainNotification(message.MessageType, "Voucher não encontrado!"));
                return;
            }

            var voucherApplied = order.ApplyVoucher(voucher);
            if (!voucherApplied.IsValid)
            {
                foreach (var error in voucherApplied.Errors)
                {
                    await _bus.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
                }

                return;
            }

            //Atualizar voucher como utilizado??? Aqui ou ao finalizar

            _orderRepository.Update(order);

            order.AddEvent(new OrderVoucherAppliedEvent(order.Id, message.ClientId, order.TotalValue));
            order.AddEvent(new OrderUpdatedEvent(order.Id, message.ClientId, order.TotalValue));

            await _orderRepository.UnitOfWork.Commit();
        }

        public async Task Handle(StartOrderCommand message)
        {
            if (!IsValid(message)) return;

            var order = await _orderRepository.GetDraftOrderByClientId(message.ClientId);

            if (order == null)
            {
                await _bus.PublishNotification(new DomainNotification(message.MessageType, "Pedido não encontrado!"));
                return;
            }

            order.StartOrder();

            //var listOfItemDto = new List<ItemDto>();
            //order.OrderItems.ForEach(p => listOfItemDto.Add(new ItemDto { CourseId = p.CourseId }));
            //var coursesOrder = new CoursesOrderDto { OrderId = order.Id, Items = listOfItemDto };

            // order.AddIntegrationEvent(new OrderStartedEvent(order.Id, order.ClientId, order.TotalValue, coursesOrder, message.NameCard, message.NumberCard, message.ExpirationDateCard, message.CvvCard));

            await _bus.PublishIntegrationEvent(
                new OrderStartedEvent(order.Id, 
                order.ClientId, 
                order.TotalValue, 
                message.NameCard, 
                message.NumberCard, 
                message.ExpirationDateCard, 
                message.CvvCard, 
                order.OrderItems.Select(p => p.CourseId).ToList())
            );

            _orderRepository.Update(order);

            await _orderRepository.UnitOfWork.Commit();
        }

        public async Task Handle(FinishOrderCommand message)
        {
            if (!IsValid(message)) return;

            var order = await _orderRepository.GetById(message.OrderId);

            if (order == null)
            {
                await _bus.PublishNotification(new DomainNotification(message.MessageType, "Pedido não encontrado!"));
                return;
            }

            order.FinishOrder();

            // order.AddIntegrationEvent(new OrderFinishedEvent(order.Id));

            await _bus.PublishIntegrationEvent(new OrderFinishedEvent(order.Id));

            await _orderRepository.UnitOfWork.Commit();
        }

        public async Task Handle(CancelOrderAndDisrollFromCourseCommand message)
        {
            if (!IsValid(message)) return;

            var order = await _orderRepository.GetById(message.OrderId);

            if (order == null)
            {
                await _bus.PublishNotification(new DomainNotification(message.MessageType, "Pedido não encontrado!"));
                return;
            }

            //var listOfItemDto = new List<ItemDto>();
            //order.OrderItems.ForEach(p => listOfItemDto.Add(new ItemDto { CourseId = p.CourseId }));
            //var coursesOrder = new CoursesOrderDto { OrderId = order.Id, Items = listOfItemDto };

            // order.AddIntegrationEvent(new OrderCanceledEvent(message.OrderId, message.ClientId, coursesOrder));

            await _bus.PublishIntegrationEvent(
                new OrderCanceledEvent(message.OrderId,
                message.ClientId,
                order.OrderItems.Select(p => p.CourseId).ToList())
            );

            order.MakeDraft();

            await _orderRepository.UnitOfWork.Commit();
        }

        public async Task Handle(CancelOrderCommand message)
        {
            if (!IsValid(message)) return;

            var order = await _orderRepository.GetById(message.OrderId);

            if (order == null)
            {
                await _bus.PublishNotification(new DomainNotification(message.MessageType, "Pedido não encontrado!"));
                return;
            }

            //var listOfItemDto = new List<ItemDto>();
            //order.OrderItems.ForEach(p => listOfItemDto.Add(new ItemDto { CourseId = p.CourseId }));
            //var coursesOrder = new CoursesOrderDto { OrderId = order.Id, Items = listOfItemDto };

            // order.AddIntegrationEvent(new OrderCanceledEvent(message.OrderId, message.ClientId, coursesOrder));

            await _bus.PublishIntegrationEvent(
                new OrderCanceledEvent(message.OrderId, 
                message.ClientId, 
                order.OrderItems.Select(p => p.CourseId).ToList())
            );

            order.MakeDraft();

            await _orderRepository.UnitOfWork.Commit();
        }

        private bool IsValid(Command message)
        {
            if (message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _bus.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
