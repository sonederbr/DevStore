using System.Threading.Tasks;

using DevStore.Core.DomainObjects.DTO;

namespace DevStore.Finance.Business
{
    public interface IPaymentService
    {
        Task<Transaction> ExectureOrderPayment(PaymentOrderDto paymentOrder);
    }
}