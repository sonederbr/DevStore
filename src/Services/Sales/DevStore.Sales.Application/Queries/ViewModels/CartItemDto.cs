using System;

namespace DevStore.Sales.Application.Queries.ViewModels
{
    public class CartItemDto
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public int Quantity { get; set; }
        public decimal UnityValue { get; set; }
        public decimal Total { get; set; }

    }
}