using System;
using System.Threading.Tasks;

using DevStore.Catalog.Application.Services;
using DevStore.Core.Bus;
using DevStore.Sales.Application.Commands;
using DevStore.Sales.Application.Queries;
using DevStore.WebApp.MVC.Controllers;

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
                              IMediatorHandler mediatorHandler) : base(mediatorHandler)
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


            return View("Index", await _orderQueries.GetCartByClient(ClientId));
        }
    }
}