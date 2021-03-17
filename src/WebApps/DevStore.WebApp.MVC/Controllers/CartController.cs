using System;
using System.Threading.Tasks;

using DevStore.Catalog.Application.Services;
using DevStore.Communication.Mediator;
using DevStore.Core.Messages.CommonMessages.Notifications;
using DevStore.Sales.Application.Commands;
using DevStore.Sales.Application.Queries;
using DevStore.Sales.Application.Queries.Dtos;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace DevStore.WebApp.MVC.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICourseAppService _courseAppService;
        private readonly IOrderQueries _orderQueries;
        private readonly IMediatorHandler _mediatorHandler;

        public CartController(ICourseAppService courseAppService,
                              IOrderQueries orderQueries,
                              INotificationHandler<DomainNotification> notifications,
                              IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _courseAppService = courseAppService;
            _orderQueries = orderQueries;
            _mediatorHandler = mediatorHandler;
        }

        [Route("my-cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _orderQueries.GetCartByClient(ClientId));
        }

        [HttpPost]
        [Route("my-cart")]
        public async Task<IActionResult> AddItem(Guid id)
        {
            var course = await _courseAppService.GetById(id);
            if (course == null) return BadRequest();

            var command = new AddOrderItemCommand(ClientId, course.Id, course.Name, course.Price);
            await _mediatorHandler.SendCommand(command);

            if (IsValidOperation())
            {
                return RedirectToAction("Index");
            }

            TempData["Errors"] = this.GetErrors();
            return RedirectToAction("CourseDetail", "ShowRoom", new { id });
        }

        [HttpPost]
        [Route("remove-item")]
        public async Task<IActionResult> RemoveItem(Guid id)
        {
            var course = await _courseAppService.GetById(id);
            if (course == null) return BadRequest();

            var command = new RemoveOrderItemCommand(ClientId, id);
            await _mediatorHandler.SendCommand(command);

            if (IsValidOperation())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderQueries.GetCartByClient(ClientId));
        }

        [HttpPost]
        [Route("apply-voucher")]
        public async Task<IActionResult> ApplyVoucher(string voucherCode)
        {
            var command = new ApplyVoucherOrderCommand(ClientId, voucherCode);
            await _mediatorHandler.SendCommand(command);

            if (IsValidOperation())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderQueries.GetCartByClient(ClientId));
        }

        [Route("order-summary")]
        public async Task<IActionResult> OrderSummary()
        {
            var cartDto = await _orderQueries.GetCartByClient(ClientId);

            // TODO: Remove after dev test
            cartDto.Payment = new CardPaymentDto
            {
                NameCard = "Ederson Lima",
                NumberCard = "1111222233334444",
                ExpirationDateCard = "10/28",
                CvvCard = "123"
            };
            return View(cartDto);
        }

        [HttpPost]
        [Route("start-order")]
        public async Task<IActionResult> StartOrder(CartDto cartDto)
        {
            var cart = await _orderQueries.GetCartByClient(ClientId);

            var command = new StartOrderCommand(ClientId, cart.OrderId, cart.Total, cartDto.Payment.NameCard,
                cartDto.Payment.NumberCard, cartDto.Payment.ExpirationDateCard, cartDto.Payment.CvvCard);

            await _mediatorHandler.SendCommand(command);

            if (IsValidOperation())
            {
                return RedirectToAction("Index", "Order");
            }

            // TODO: Remove after dev test
            cart.Payment = cartDto.Payment;
            
            return View("OrderSummary", cart);
        }
    }
}