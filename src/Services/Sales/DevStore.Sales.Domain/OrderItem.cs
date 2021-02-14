using System;
using DevStore.Core.DomainObjects;

namespace DevStore.Sales.Domain
{
    public class OrderItem : Entity
    {
        public Guid OrderId { get; private set; }
        public Guid CourseId { get; private set; }
        public string CourseName { get; private set; }
        public int Quantity { get; private set; }
        public decimal ItemValue { get; private set; }

        // EF Rel.
        public Order Order { get; set; }

        protected OrderItem() { }

        public OrderItem(Guid courseId, string courseName, int quantity, decimal itemValue)
        {
            CourseId = courseId;
            CourseName = courseName;
            Quantity = quantity;
            ItemValue = itemValue;
        }

        internal void AssociateOrder(Guid orderId)
        {
            OrderId = orderId;
        }

        public decimal CalculateValue()
        {
            return Quantity * ItemValue;
        }

        internal void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }

        internal void UpdateOfQuantityOfItems(int quantity)
        {
            Quantity = quantity;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}