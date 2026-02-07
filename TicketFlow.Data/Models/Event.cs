using System.Net.Sockets;

namespace TicketFlow.Data.Models
{
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Location { get; set; } = null!;

        public DateTime Date { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<EventCategory> EventCategories { get; set; } = new List<EventCategory>();
    }
}
