﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevStore.Sales.Application.Queries.ViewModels;
using DevStore.Sales.Domain;

namespace DevStore.Sales.Application.Queries
{
    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueries(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CartDto> GetCartByClient(Guid clientId)
        {
            var order = await _orderRepository.GetDraftOrderByClientId(clientId);
            if (order == null) return null;

            var carrinho = new CartDto
            {
                ClientId = order.ClientId,
                Total = order.TotalValue,
                OrderId = order.Id,
                Discount = order.Discount,
                SubTotal = order.Discount + order.TotalValue
            };

            if (order.VoucherId != null)
            {
                carrinho.VoucherCode = order.Voucher.Code;
            }

            foreach (var item in order.OrderItems)
            {
                carrinho.Items.Add(new CartItemDto
                {
                    CourseId = item.CourseId,
                    CourseName = item.CourseName,
                    Quantity = item.Quantity,
                    UnityValue = item.ItemValue,
                    Total = item.ItemValue * item.Quantity
                });
            }

            return carrinho;
        }

        public async Task<IEnumerable<OrderDto>> GetOrderByClient(Guid clientId)
        {
            var orders = await _orderRepository.GetByClientId(clientId);

            orders = orders.Where(p => p.OrderStatus == OrderStatus.Billed || p.OrderStatus == OrderStatus.Canceled)
                .OrderByDescending(p => p.Code);

            if (!orders.Any()) return null;

            var ordersDto = new List<OrderDto>();

            foreach (var pedido in orders)
            {
                ordersDto.Add(new OrderDto
                {
                    Total = pedido.TotalValue,
                    OrderStatus = (int)pedido.OrderStatus,
                    Code = pedido.Code,
                    CreatedDate = pedido.CreatedDate
                });
            }

            return ordersDto;
        }
    }
}