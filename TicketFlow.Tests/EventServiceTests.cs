using Microsoft.EntityFrameworkCore;
using TicketFlow.Data;
using TicketFlow.Data.Models;
using TicketFlow.Services.Implementations;

public class EventServiceTests
{
    private TicketFlowDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<TicketFlowDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new TicketFlowDbContext(options);
    }

    [Fact]
    public async Task GetAllEventsAsync_ShouldReturnAllEvents()
    {
        var context = GetDbContext();

        context.Events.Add(new Event { Name = "A", Description = "D", Location = "L", Date = DateTime.Now });
        context.Events.Add(new Event { Name = "B", Description = "D2", Location = "L2", Date = DateTime.Now });
        await context.SaveChangesAsync();

        var service = new EventService(context);

        var result = await service.GetAllEventsAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetEventByIdAsync_ShouldReturnCorrectEvent()
    {
        var context = GetDbContext();

        var ev = new Event { Name = "Test", Description = "D", Location = "L", Date = DateTime.Now };
        context.Events.Add(ev);
        await context.SaveChangesAsync();

        var service = new EventService(context);

        var result = await service.GetEventByIdAsync(ev.Id);

        Assert.NotNull(result);
        Assert.Equal("Test", result.Name);
    }

    [Fact]
    public async Task CreateEventAsync_ShouldAddEventToDatabase()
    {
        var context = GetDbContext();
        var service = new EventService(context);

        var ev = new Event { Name = "New", Description = "D", Location = "L", Date = DateTime.Now };

        await service.CreateEventAsync(ev);

        Assert.Equal(1, context.Events.Count());
    }
    [Fact]
    public async Task GetEventByIdAsync_ShouldReturnNull_WhenEventDoesNotExist()
    {
        var context = GetDbContext();
        var service = new EventService(context);

        var result = await service.GetEventByIdAsync(999);

        Assert.Null(result);
    }

}
