using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net.Sockets;
using TicketFlow.Data.Models;


namespace TicketFlow.Data
{
    public class TicketFlowDbContext : DbContext
    {
        public TicketFlowDbContext(DbContextOptions<TicketFlowDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EventCategory>()
                .HasKey(ec => new { ec.EventId, ec.CategoryId });

            modelBuilder.Entity<Ticket>()
                .Property(t => t.Price)
                .HasPrecision(18, 2);
        }

    }
}
