using System.Threading;
using System.Threading.Tasks;

using DevStore.Core.DomainObjects.DTO;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;

using MediatR;

namespace DevStore.Finance.Business.Events
{
    public class PaymentEventHandler : INotificationHandler<OrderEnrolledAcceptedEvent>
    {
        private readonly IPaymentService _paymentService;

        public PaymentEventHandler(IPaymentService pagamentoService)
        {
            _paymentService = pagamentoService;
        }

        public async Task Handle(OrderEnrolledAcceptedEvent message, CancellationToken cancellationToken)
        {
            var pagamentoPedido = new PaymentOrderDto
            {
                OrderId = message.OrderId,
                ClientId = message.ClientId,
                Total = message.Total,
                NameCard = message.NameCard,
                NumberCard = message.NumberCard,
                ExpirationDateCard = message.ExpirationDateCard,
                CvvCard = message.CvvCard
            };

            await _paymentService.ExectureOrderPayment(pagamentoPedido);
        }
    }
}