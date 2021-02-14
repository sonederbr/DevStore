using System;
using DevStore.Core.DomainObjects;

namespace DevStore.Sales.Domain
{
    public class OrderItem : Entity
    {
        public Guid OrderId { get; private set; }
        public Guid CourseId { get; private set; }
        public string CourseName { get; private set; }
        public decimal Price { get; private set; }

        // EF Rel.
        public Order Order { get; set; }

        protected OrderItem() { }

        public OrderItem(Guid courseId, string courseName, decimal price)
        {
            CourseId = courseId;
            CourseName = courseName;
            Price = price;
        }

        internal void AssociateOrder(Guid orderId)
        {
            OrderId = orderId;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}