using Microsoft.EntityFrameworkCore;
using TicketFlow.Data;
using TicketFlow.Data.Models;
using TicketFlow.Services.Interfaces;

namespace TicketFlow.Services.Implementations
{
    public class EventService : IEventService
    {
        private readonly TicketFlowDbContext _context;

        public EventService(TicketFlowDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events
                .Include(e => e.Tickets)
                .Include(e => e.EventCategories)
                .ToListAsync();
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            return await _context.Events
                .Include(e => e.Tickets)
                .Include(e => e.EventCategories)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateEventAsync(Event ev)
        {
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
        }
    }
}
