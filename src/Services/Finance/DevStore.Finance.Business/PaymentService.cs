using System.Threading.Tasks;

using DevStore.Communication.Mediator;
using DevStore.Core.DomainObjects.DTO;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;
using DevStore.Core.Messages.CommonMessages.Notifications;

namespace DevStore.Finance.Business
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentCreditCardFacade _paymentCreditCardFacade;
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public PaymentService(IPaymentCreditCardFacade paymentCreditCardFacade,
                              IOrderRepository orderRepository,
                              IMediatorHandler mediatorHandler)
        {
            _paymentCreditCardFacade = paymentCreditCardFacade;
            _orderRepository = orderRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<Transaction> ExectureOrderPayment(PaymentOrderDto paymentOrder)
        {
            var order = new Order
            {
                Id = paymentOrder.OrderId,
                ClientId = paymentOrder.ClientId,
                Total = paymentOrder.Total
            };

            var payment = new Payment
            {
                Total = paymentOrder.Total,
                NameCard = paymentOrder.NameCard,
                NumberCard = paymentOrder.NumberCard,
                ExpirationDateCard = paymentOrder.ExpirationDateCard,
                CvvCard = paymentOrder.CvvCard,
                OrderId = paymentOrder.OrderId
            };

            var transaction = _paymentCreditCardFacade.ExecutePayment(order, payment);

            if (transaction.StatusTransaction == StatusTransaction.Paid)
            {
                payment.AddEvent(new PaymentRealizedEvent(payment.Id, transaction.Id, order.Id, order.ClientId, order.Total));

                _orderRepository.Add(payment);
                _orderRepository.Add(transaction);

                await _orderRepository.UnitOfWork.Commit();
                return transaction;
            }

            await _mediatorHandler.PublishNotification(new DomainNotification(this.GetType().Name, "A operadora recusou o pagamento"));
            await _mediatorHandler.PublishEvent(new PaymentRefusedEvent(payment.Id, transaction.Id, order.Id, order.ClientId, order.Total));

            return transaction;
        }
    }
}