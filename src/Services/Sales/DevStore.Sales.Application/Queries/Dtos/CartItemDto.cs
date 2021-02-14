using System;

namespace DevStore.Sales.Application.Queries.Dtos
{
    public class CartItemDto
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }
    }
}