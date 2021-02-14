using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevStore.Sales.Application.Queries.ViewModels;

namespace DevStore.Sales.Application.Queries
{
    public interface IOrderQueries
    {
        Task<CartDto> GetCartByClient(Guid clientId);
        Task<IEnumerable<OrderDto>> GetOrderByClient(Guid clientId);
    }
}