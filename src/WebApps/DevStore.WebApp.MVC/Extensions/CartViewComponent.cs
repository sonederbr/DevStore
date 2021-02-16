using System;
using System.Threading.Tasks;

using DevStore.Sales.Application.Queries;

using Microsoft.AspNetCore.Mvc;

namespace DevStore.WebApp.MVC.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IOrderQueries _orderQueries;

        // TODO: Obter cliente logado
        protected Guid ClienteId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");


        public CartViewComponent(IOrderQueries orderQueries)
        {
            _orderQueries = orderQueries;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = await _orderQueries.GetCartByClient(ClienteId);
            var items = cart?.Items.Count ?? 0;

            return View(items);
        }
    }
}