using Microsoft.EntityFrameworkCore;
using TicketFlow.Data;
using TicketFlow.Data.Models;
using TicketFlow.Services.Interfaces;

<<<<<<< HEAD
namespace TicketFlow.Services.Implementations 
{ 
    public class EventService : IEventService 
    { 
        private readonly TicketFlowDbContext db; 
        
        public EventService(TicketFlowDbContext context) 
        {
            this.db = context;
        } 
        public async Task<IEnumerable<Event>> GetAllEventsAsync() 
        {
            return await db.Events.ToListAsync();
        } 
        public async Task<Event?> GetEventByIdAsync(int id) 
        { 
            return await db.Events.FirstOrDefaultAsync(e => e.Id == id);
        } 
        public async Task<Event> CreateEventAsync(Event newEvent) 
        { 
            db.Events.Add(newEvent); 
            await db.SaveChangesAsync(); 
            return newEvent; 
        } 
        public async Task<bool> UpdateEventAsync(Event updatedEvent) 
        { 
            var existing = await db.Events.FindAsync(updatedEvent.Id); 
            if (existing == null) 
                return false; 
            
            existing.Name = updatedEvent.Name; 
            existing.Description = updatedEvent.Description; 
            existing.Date = updatedEvent.Date; 
            existing.Location = updatedEvent.Location; 
            
            await db.SaveChangesAsync(); 
            return true; 
        } 
        public async Task<bool> DeleteEventAsync(int id) 
        { 
            var existing = await db.Events.FindAsync(id); 
            if (existing == null)
                return false; 
            
            db.Events.Remove(existing); 
            await db.SaveChangesAsync(); 
            return true;
=======
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
>>>>>>> eecc1bbf71c5df66ab798a52c353a2446c9b5ada
        }
    }
}
