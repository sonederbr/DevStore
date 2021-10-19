using System.Threading.Tasks;

using DevStore.Core.Communication.Bus;
using DevStore.Core.DomainObjects.DTO;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;
using DevStore.Core.Messages.CommonMessages.Notifications;

namespace DevStore.Finance.Business
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentCreditCardFacade _paymentCreditCardFacade;
        private readonly IOrderRepository _orderRepository;
        private readonly IBusHandler _bus;

        public PaymentService(IPaymentCreditCardFacade paymentCreditCardFacade,
                              IOrderRepository orderRepository,
                              IBusHandler bus)
        {
            _paymentCreditCardFacade = paymentCreditCardFacade;
            _orderRepository = orderRepository;
            _bus = bus;
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
                // payment.AddIntegrationEvent(new PaymentRealizedEvent(payment.Id, transaction.Id, order.Id, order.ClientId, order.Total));
                await _bus.PublishIntegrationEvent(new PaymentRealizedEvent(payment.Id, transaction.Id, order.Id, order.ClientId, order.Total));

                payment.Status = transaction.StatusTransaction.ToString();
                _orderRepository.Add(payment);
                _orderRepository.Add(transaction);

                await _orderRepository.UnitOfWork.Commit();
                return transaction;
            }

            await _bus.PublishNotification(new DomainNotification(this.GetType().Name, "A operadora recusou o pagamento"));
            await _bus.PublishIntegrationEvent(new PaymentRefusedEvent(payment.Id, transaction.Id, order.Id, order.ClientId, order.Total));
            
            payment.Status = transaction.StatusTransaction.ToString();

            return transaction;
        }
    }
}