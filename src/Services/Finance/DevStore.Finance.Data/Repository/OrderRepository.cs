using DevStore.Core.Data;
using DevStore.Finance.Business;

namespace DevStore.Finance.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FinanceContext _context;

        public OrderRepository(FinanceContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;


        public void Add(Payment payment)
        {
            _context.Payments.Add(payment);
        }

        public void Add(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}