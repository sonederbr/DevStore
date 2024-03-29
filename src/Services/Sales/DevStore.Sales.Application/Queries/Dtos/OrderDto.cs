﻿using System;

namespace DevStore.Sales.Application.Queries.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public decimal Total { get; set; }
        public int OrderStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}