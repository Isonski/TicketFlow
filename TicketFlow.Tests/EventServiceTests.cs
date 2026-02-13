using Microsoft.EntityFrameworkCore;
using TicketFlow.Data;
using TicketFlow.Data.Models;
using TicketFlow.Services.Implementations;
using Xunit;

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
    public async Task GetAllEventsAsync_ReturnsAllEvents()
    {
        var context = GetDbContext();
        context.Events.Add(new Event { Name = "Test Event" });
        await context.SaveChangesAsync();

        var service = new EventService(context);

        var result = await service.GetAllEventsAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task CreateEventAsync_AddsEventToDatabase()
    {
        var context = GetDbContext();
        var service = new EventService(context);

        var ev = new Event { Name = "New Event" };

        await service.CreateEventAsync(ev);

        Assert.Equal(1, context.Events.Count());
    }
    [Fact]
    public async Task GetEventByIdAsync_ReturnsCorrectEvent()
    {
        var context = GetDbContext();

        var ev = new Event { Id = 1, Name = "Concert" };
        context.Events.Add(ev);
        await context.SaveChangesAsync();

        var service = new EventService(context);

        var result = await service.GetEventByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Concert", result.Name);
    }

    [Fact]
    public async Task GetEventByIdAsync_ReturnsNull_WhenEventDoesNotExist()
    {
        var context = GetDbContext();
        var service = new EventService(context);

        var result = await service.GetEventByIdAsync(999);

        Assert.Null(result);
    }

}
