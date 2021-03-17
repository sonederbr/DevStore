using System.Threading.Tasks;

using DevStore.Communication.Mediator;
using DevStore.Core.Messages.CommonMessages.Notifications;
using DevStore.Sales.Application.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace DevStore.WebApp.MVC.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderQueries _orderQueries;

        public OrderController(IOrderQueries orderQueries,
                               INotificationHandler<DomainNotification> notifications,
                               IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _orderQueries = orderQueries;
        }

        [Route("my-orders")]
        public async Task<IActionResult> Index()
        {
            return View(await _orderQueries.GetOrderByClient(ClientId));
        }
    }
}