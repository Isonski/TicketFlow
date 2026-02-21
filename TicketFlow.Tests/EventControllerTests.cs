using Microsoft.AspNetCore.Mvc;
using Moq;
using TicketFlow.Data.Models;
using TicketFlow.Services.Interfaces;
using TicketFlow.Web.Controllers;

public class EventControllerTests
{
    [Fact]
    public async Task Index_ShouldReturnViewWithEvents()
    {
        var mockService = new Mock<IEventService>();
        mockService.Setup(s => s.GetAllEventsAsync())
            .ReturnsAsync(new List<Event> { new Event { Name = "Test" } });

        var controller = new EventController(mockService.Object);

        var result = await controller.Index();

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Event>>(viewResult.Model);

        Assert.Single(model);
    }

    [Fact]
    public async Task Create_Post_ShouldReturnView_WhenModelStateInvalid()
    {
        var mockService = new Mock<IEventService>();
        var controller = new EventController(mockService.Object);

        controller.ModelState.AddModelError("Name", "Required");

        var ev = new Event();

        var result = await controller.Create(ev);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(ev, viewResult.Model);
    }

    [Fact]
    public async Task Details_ShouldReturnNotFound_WhenEventDoesNotExist()
    {
        var mockService = new Mock<IEventService>();
        mockService.Setup(s => s.GetEventByIdAsync(5))
            .ReturnsAsync((Event?)null);

        var controller = new EventController(mockService.Object);

        var result = await controller.Details(5);

        Assert.IsType<NotFoundResult>(result);
    }
    [Fact]
    public async Task Details_ShouldReturnView_WhenEventExists()
    {
        var mockService = new Mock<IEventService>();
        mockService.Setup(s => s.GetEventByIdAsync(1))
            .ReturnsAsync(new Event { Id = 1, Name = "Concert" });

        var controller = new EventController(mockService.Object);

        var result = await controller.Details(1);

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Event>(viewResult.Model);

        Assert.Equal("Concert", model.Name);
    }
    [Fact]
    public async Task Create_Post_ShouldRedirectToIndex_WhenModelStateValid()
    {
        var mockService = new Mock<IEventService>();
        var controller = new EventController(mockService.Object);

        var ev = new Event
        {
            Name = "Test",
            Description = "D",
            Location = "L",
            Date = DateTime.Now
        };

        var result = await controller.Create(ev);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
    }

}
