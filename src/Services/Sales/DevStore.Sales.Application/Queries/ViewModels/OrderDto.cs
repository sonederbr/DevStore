using System;

namespace DevStore.Sales.Application.Queries.ViewModels
{
    public class OrderDto
    {
        public int Code { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedDate { get; set; }
        public int OrderStatus { get; set; }
    }
}