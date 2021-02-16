using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using DevStore.Communication.Mediator;
using DevStore.Core.Messages;
using DevStore.Core.Messages.CommonMessages.Notifications;
using DevStore.Sales.Domain;

using MediatR;

namespace DevStore.Sales.Application.Commands
{
    public class OrderCommandHandler :
        IRequestHandler<AddOrderItemCommand, bool>,
        IRequestHandler<RemoveOrderItemCommand, bool>

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
            }

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
            //pedido.AdicionarEvento(new PedidoAtualizadoEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal));
            //pedido.AdicionarEvento(new PedidoProdutoRemovidoEvent(message.ClienteId, pedido.Id, message.ProdutoId));

            _orderRepository.RemoveItem(orderItem);
            _orderRepository.Update(order);

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
