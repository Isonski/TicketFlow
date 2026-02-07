using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TicketFlow.Data
{
    public class TicketFlowDbContextFactory : IDesignTimeDbContextFactory<TicketFlowDbContext>
    {
        public TicketFlowDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TicketFlowDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=TicketFlowDb;Trusted_Connection=True;");

            return new TicketFlowDbContext(optionsBuilder.Options);
        }
    }
}
