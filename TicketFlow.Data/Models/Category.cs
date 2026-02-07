namespace TicketFlow.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<EventCategory> EventCategories { get; set; } = new List<EventCategory>();
    }
}
