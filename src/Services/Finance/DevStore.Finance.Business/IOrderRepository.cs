using DevStore.Core.Data;

namespace DevStore.Finance.Business
{
    public interface IOrderRepository : IRepository<Payment>
    {
        void Add(Payment payment);

        void Add(Transaction transaction);
    }
}