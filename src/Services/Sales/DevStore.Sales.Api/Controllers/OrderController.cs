using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using DevStore.Core.Communication.Bus;
using DevStore.Core.Messages.CommonMessages.Notifications;
using DevStore.Sales.Application;
using DevStore.Sales.Application.Commands;
using DevStore.Sales.Application.Queries;
using DevStore.Sales.Application.Queries.Dtos;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Rebus.Bus;

namespace DevStore.Sales.Api.Controllers
{
    [Route("api/order")]
    public class OrderController : BaseController
    {
        private readonly IBusHandler _bus;

        private readonly IOrderQueries _orderQueries;

        public OrderController(IOrderQueries orderQueries,
                               IBusHandler bus) : base()
        {
            _orderQueries = orderQueries;
            _bus = bus;
        }

        [HttpGet]
        public async Task<CartDto> GetAsync()
        {
            var cart = await _orderQueries.GetCartByClient(ClientId);
            return cart;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(CartDto cartDto)
        {
            var cart = await _orderQueries.GetCartByClient(ClientId);
            if (cart == null)
                return BadRequest();

            cart.Payment = new CardPaymentDto
            {
                NameCard = "Ederson Lima",
                NumberCard = "1111222233334444",
                ExpirationDateCard = "10/28",
                CvvCard = "123"
            };

            var command = new StartSagaCommand(ClientId, cart.OrderId, cart.Total, cart.Payment.NameCard,
                cart.Payment.NumberCard, cart.Payment.ExpirationDateCard, cart.Payment.CvvCard);

            _bus.SendCommand(command).Wait();

            return Accepted();
        }
    }
}
