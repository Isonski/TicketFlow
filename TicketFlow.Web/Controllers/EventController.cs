using Microsoft.AspNetCore.Mvc;
using TicketFlow.Services.Interfaces;
using TicketFlow.Services.Implementations;
using TicketFlow.Data;
using TicketFlow.Data.Models;


namespace TicketFlow.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAllEventsAsync();
            return View(events);
        }
        // GET: Event/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        public async Task<IActionResult> Create(Event ev)
        {
            if (!ModelState.IsValid)
                return View(ev);

            await _eventService.CreateEventAsync(ev);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var ev = await _eventService.GetEventByIdAsync(id);

            if (ev == null)
                return NotFound();

            return View(ev);
        }

    }

}
