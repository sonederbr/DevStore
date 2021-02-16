using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DevStore.Core.Data;

namespace DevStore.Sales.Domain
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetById(Guid id);
        Task<IEnumerable<Order>> GetByClientId(Guid clientId);
        Task<Order> GetDraftOrderByClientId(Guid clientId);
        void Add(Order order);
        void Update(Order order);
        void Remove(Order order);

        Task<OrderItem> GetItemById(Guid id);
        Task<OrderItem> GetItemByOrderId(Guid orderId, Guid courseId);
        void AddItem(OrderItem orderItem);
        void UpdateItem(OrderItem orderItem);
        void RemoveItem(OrderItem orderItem);

        Task<Voucher> GetVoucherByCode(string code);
    }
}