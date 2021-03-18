﻿using System;

using DevStore.Core.Messages;

namespace DevStore.Sales.Application.Events
{
    public class OrderUpdatedEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public decimal TotalValue { get; private set; }

        public OrderUpdatedEvent(Guid orderId, Guid clientId, decimal totalValue)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
            TotalValue = totalValue;
        }
    }
}
