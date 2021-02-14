using System;
using System.Collections.Generic;

namespace DevStore.Sales.Application.Queries.Dtos
{
    public class CartDto
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public string VoucherCode { get; set; }

        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
        public CartPaymentDto Payment { get; set; }
    }
}