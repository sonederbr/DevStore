using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevStore.Catalog.Application.Services;
using DevStore.Catalog.Application.Dtos;

namespace DevStore.WebApp.MVC.Controllers.Admin
{
    public class AdminCoursesController : Controller
    {
        private readonly ICourseAppService _courseAppService;

        public AdminCoursesController(ICourseAppService courseAppService)
        {
            _courseAppService = courseAppService;
        }

        [HttpGet]
        [Route("admin-courses")]
        public async Task<IActionResult> Index()
        {
            return View(await _courseAppService.GetAll());
        }

        [Route("new-course")]
        public async Task<IActionResult> NewCourse()
        {
            return View(await FillCategories(new CourseDto()));
        }

        [Route("new-course")]
        [HttpPost]
        public async Task<IActionResult> NewCourse(CourseDto courseDto)
        {
            if (!ModelState.IsValid) return View(await FillCategories(courseDto));

            await _courseAppService.CreateCourse(courseDto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("edit-course")]
        public async Task<IActionResult> EditCourse(Guid id)
        {
            return View(await FillCategories(await _courseAppService.GetById(id)));
        }

        [HttpPost]
        [Route("edit-course")]
        public async Task<IActionResult> EditCourse(Guid id, CourseDto courseDto)
        {
            var course = await _courseAppService.GetById(id);
            courseDto.PlacesAvailable = course.PlacesAvailable;

            ModelState.Remove("PlacesAvailable");
            if (!ModelState.IsValid) return View(await FillCategories(courseDto));

            await _courseAppService.UpdateCourse(courseDto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("course-update-slots")]
        public async Task<IActionResult> UpdateSlot(Guid id)
        {
            return View("Slot", await _courseAppService.GetById(id));
        }

        [HttpPost]
        [Route("course-update-slots")]
        public async Task<IActionResult> UpdateSlot(Guid id, int quantidade)
        {
            if (quantidade > 0)
            {
                await _courseAppService.ChargeStock(id, quantidade);
            }
            else
            {
                await _courseAppService.WithdrawStock(id, quantidade);
            }

            return View("Index", await _courseAppService.GetAll());
        }

        private async Task<CourseDto> FillCategories(CourseDto produto)
        {
            produto.Categories = await _courseAppService.GetCategories();
            return produto;
        }
    }
}