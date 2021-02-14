using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevStore.Catalog.Application.Services;

namespace DevStore.WebApp.MVC.Controllers
{
    public class ShowRoomController : Controller
    {
        private readonly ICourseAppService _courseAppService;

        public ShowRoomController(ICourseAppService courseAppService)
        {
            _courseAppService = courseAppService;
        }

        [HttpGet]
        [Route("")]
        [Route("show-room")]
        public async Task<IActionResult> Index()
        {
            return View(await _courseAppService.GetAll());
        }

        [HttpGet]
        [Route("course-detail/{id}")]
        public async Task<IActionResult> CourseDetail(Guid id)
        {
            return View(await _courseAppService.GetById(id));
        }
    }
}