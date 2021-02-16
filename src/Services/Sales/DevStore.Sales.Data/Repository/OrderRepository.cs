using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DevStore.Catalog.Data;
using DevStore.Core.Data;
using DevStore.Sales.Domain;

using Microsoft.EntityFrameworkCore;

namespace DevStore.Sales.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SalesContext _context;

        public OrderRepository(SalesContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Order> GetById(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetByClientId(Guid clientId)
        {
            return await _context.Orders.AsNoTracking().Where(p => p.ClientId == clientId).ToListAsync();
        }

        public async Task<Order> GetDraftOrderByClientId(Guid clientId)
        {
            var pedido = await _context.Orders.FirstOrDefaultAsync(p => p.ClientId == clientId && p.OrderStatus == OrderStatus.Draft);
            if (pedido == null) return null;

            await _context.Entry(pedido)
                .Collection(i => i.OrderItems).LoadAsync();

            if (pedido.VoucherId != null)
            {
                await _context.Entry(pedido)
                    .Reference(i => i.Voucher).LoadAsync();
            }

            return pedido;
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }

        public void Remove(Order order)
        {
            _context.Orders.Remove(order);
        }

        public async Task<OrderItem> GetItemById(Guid id)
        {
            return await _context.OrderItems.FindAsync(id);
        }

        public async Task<OrderItem> GetItemByOrderId(Guid orderId, Guid courseId)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(p => p.CourseId  == courseId && p.OrderId == orderId);
        }

        public void AddItem(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
        }

        public void UpdateItem(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
        }

        public void RemoveItem(OrderItem orderItem)
        {
            _context.OrderItems.Remove(orderItem);
        }

        public async Task<Voucher> GetVoucherByCode(string code)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(p => p.Code == code);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
