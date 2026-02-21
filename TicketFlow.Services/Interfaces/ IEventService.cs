using TicketFlow.Data.Models;
namespace TicketFlow.Services.Interfaces 
{ public interface IEventService 
    { Task<IEnumerable<Event>> GetAllEventsAsync(); 
        Task<Event?> GetEventByIdAsync(int id); 
        Task<Event> CreateEventAsync(Event newEvent); 
        Task<bool> UpdateEventAsync(Event updatedEvent); 
        Task<bool> DeleteEventAsync(int id); } 
}