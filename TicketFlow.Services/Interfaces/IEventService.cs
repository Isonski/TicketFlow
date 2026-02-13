using TicketFlow.Data.Models;

namespace TicketFlow.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event?> GetEventByIdAsync(int id);
        Task CreateEventAsync(Event ev);
    }
}
