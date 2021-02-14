using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using DevStore.Core.Messages;
using DevStore.Sales.Domain;

using MediatR;

namespace DevStore.Sales.Application.Commands
{
    public class OrderCommandHandler :
        IRequestHandler<AddOrderItemCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(AddOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!IsValid(message)) return false;

            var order = await _orderRepository.GetDraftOrderByClientId(message.ClientId);
            var orderItem = new OrderItem(message.CourseId, message.CourseName, message.Quantity, message.ItemValue);

            if(order == null)
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

        private bool IsValid(Command message)
        {
            if (message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                //TODO: Send error event
            }

            return false;
        }
    }
}
