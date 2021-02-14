using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using DevStore.Core.DomainObjects;

namespace DevStore.Sales.Domain
{
    public class Order : Entity, IAggregateRoot
    {
        public int Code { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool HasVoucher { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalValue { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? UpdatedDate { get; private set; }
        public OrderStatus OrderStatus { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        // EF Rel.
        public Voucher Voucher { get; private set; }

        public Order(Guid clientId, bool hasVoucher, decimal discount, decimal totalValue)
        {
            ClientId = clientId;
            HasVoucher = hasVoucher;
            Discount = discount;
            TotalValue = totalValue;
            _orderItems = new List<OrderItem>();
        }

        protected Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public ValidationResult ApplyVoucher(Voucher voucher)
        {
            var validationResult = voucher.ValidIfIsAnable();
            if (!validationResult.IsValid) return validationResult;

            Voucher = voucher;
            HasVoucher = true;
            CalculateOrderValue();

            return validationResult;
        }

        public void CalculateOrderValue()
        {
            TotalValue = OrderItems.Sum(p => p.Price);
            CalculateOrderValueWithDiscount();
        }

        public void CalculateOrderValueWithDiscount()
        {
            if (!HasVoucher) return;

            decimal discount = 0;
            var value = TotalValue;

            if (Voucher.TypeOfDiscount == TypeOfDiscount.Percentagem)
            {
                if (Voucher.Percentage.HasValue)
                {
                    discount = (value * Voucher.Percentage.Value) / 100;
                    value -= discount;
                }
            }
            else
            {
                if (Voucher.ValueOfDiscount.HasValue)
                {
                    discount = Voucher.ValueOfDiscount.Value;
                    value -= discount;
                }
            }

            TotalValue = value < 0 ? 0 : value;
            Discount = discount;
        }

        public bool HasOrderItem(OrderItem item)
        {
            return _orderItems.Any(p => p.CourseId == item.CourseId);
        }

        public void AddItem(OrderItem item)
        {
            if (!item.IsValid()) return;

            item.AssociateOrder(Id);

            if (HasOrderItem(item))
            {
                var itemInOrder = _orderItems.FirstOrDefault(p => p.CourseId == item.CourseId);
                item = itemInOrder;

                _orderItems.Remove(itemInOrder);
            }
            _orderItems.Add(item);

            CalculateOrderValue();
        }

        public void RemoveItem(OrderItem item)
        {
            if (!item.IsValid()) return;

            var itemInOrder = OrderItems.FirstOrDefault(p => p.CourseId == item.CourseId);

            if (itemInOrder == null) throw new DomainException("O item não pertence ao order");
            _orderItems.Remove(itemInOrder);

            CalculateOrderValue();
        }

        public void UpdateItem(OrderItem item)
        {
            if (!item.IsValid()) return;
            item.AssociateOrder(Id);

            var itemInOrder = OrderItems.FirstOrDefault(p => p.CourseId == item.CourseId);

            if (itemInOrder == null) throw new DomainException("O item não pertence ao order");

            _orderItems.Remove(itemInOrder);
            _orderItems.Add(item);

            CalculateOrderValue();
        }

        public void MakeDraft()
        {
            OrderStatus = OrderStatus.Draft;
        }

        public void StartOrder()
        {
            OrderStatus = OrderStatus.Started;
        }

        public void FinishOrder()
        {
            OrderStatus = OrderStatus.Billed;
        }

        public void CancelOrder()
        {
            OrderStatus = OrderStatus.Canceled;
        }

        public static class OrderFactory
        {
            public static Order NewDraftOrder(Guid clientId)
            {
                var order = new Order
                {
                    ClientId = clientId,
                };

                order.MakeDraft();
                return order;
            }
        }
    }
}