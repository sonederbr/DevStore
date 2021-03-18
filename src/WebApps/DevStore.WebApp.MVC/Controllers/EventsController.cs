using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevStore.Core.Data.EventSourcing;

namespace DevStore.WebApp.MVC.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventSourcingRepository _eventSourcingRepository;

        public EventsController(IEventSourcingRepository eventSourcingRepository)
        {
            _eventSourcingRepository = eventSourcingRepository;
        }

        [HttpGet("events/{id:guid}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var eventos = await _eventSourcingRepository.GetEvents(id);
            return View(eventos);
        }
    }
}