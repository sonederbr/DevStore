using DevStore.Finance.Business;

namespace DevStore.Finance.AntiCorruption
{
    public class PaymentCreditCardFacade : IPaymentCreditCardFacade
    {
        private readonly IPayPalGateway _payPalGateway;
        private readonly IConfigurationManager _configManager;

        public PaymentCreditCardFacade(IPayPalGateway payPalGateway, IConfigurationManager configManager)
        {
            _payPalGateway = payPalGateway;
            _configManager = configManager;
        }

        public Transaction ExecutePayment(Order order, Payment payment)
        {
            var apiKey = _configManager.GetValue("apiKey");
            var encriptionKey = _configManager.GetValue("encriptionKey");

            var serviceKey = _payPalGateway.GetPayPalServiceKey(apiKey, encriptionKey);
            var cardHashKey = _payPalGateway.GetCardHashKey(serviceKey, payment.NumberCard);

            var pagamentoResult = _payPalGateway.CommitTransaction(cardHashKey, order.Id.ToString(), payment.Total);

            // TODO: O gateway de pagamentos que deve retornar o objeto transação
            var transacao = new Transaction
            {
                OrderId = order.Id,
                Total = order.Total,
                PaymentId = payment.Id
            };

            if (pagamentoResult)
            {
                transacao.StatusTransaction = StatusTransaction.Paid;
                return transacao;
            }

            transacao.StatusTransaction = StatusTransaction.Refused;
            return transacao;
        }
    }
}