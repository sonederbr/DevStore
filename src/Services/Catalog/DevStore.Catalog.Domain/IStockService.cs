using System;
using System.Threading.Tasks;

namespace DevStore.Catalog.Domain
{
    public interface IStockService : IDisposable
    {
        Task<bool> WithdrawStocks(Guid productId, int quantity);
        Task<bool> ChargeStock(Guid productId, int quantity);
    }
}