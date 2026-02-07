namespace TicketFlow.Data.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string Type { get; set; } = null!;
        public decimal Price { get; set; } = 0m;


        public int EventId { get; set; }
        public Event Event { get; set; } = null!;
    }
}
