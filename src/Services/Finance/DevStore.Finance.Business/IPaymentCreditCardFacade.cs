namespace DevStore.Finance.Business
{
    public interface IPaymentCreditCardFacade
    {
        Transaction ExecutePayment(Order order, Payment payment);
    }
}