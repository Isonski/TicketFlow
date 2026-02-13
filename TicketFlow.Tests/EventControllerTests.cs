using Moq;
using TicketFlow.Services.Interfaces;
using TicketFlow.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Data.Models;
using Xunit;

public class EventControllerTests
{
    [Fact]
    public async Task Index_ReturnsViewResult()
    {
        // Arrange
        var mockService = new Mock<IEventService>();
        mockService.Setup(s => s.GetAllEventsAsync())
            .ReturnsAsync(new List<Event>());

        var controller = new EventController(mockService.Object);

        // Act
        var result = await controller.Index();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task Index_ReturnsModelOfTypeListEvent()
    {
        // Arrange
        var mockService = new Mock<IEventService>();
        mockService.Setup(s => s.GetAllEventsAsync())
            .ReturnsAsync(new List<Event> { new Event { Name = "Test" } });

        var controller = new EventController(mockService.Object);

        // Act
        var result = await controller.Index() as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<Event>>(result.Model);
    }
    

    [Fact]
    public async Task Details_ReturnsNotFound_WhenEventDoesNotExist()
    {
        var mockService = new Mock<IEventService>();
        mockService.Setup(s => s.GetEventByIdAsync(999))
            .ReturnsAsync((Event?)null);

        var controller = new EventController(mockService.Object);

        var result = await controller.Details(999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Create_Post_RedirectsToIndex_WhenModelIsValid()
    {
        // Arrange
        var mockService = new Mock<IEventService>();
        var controller = new EventController(mockService.Object);

        var newEvent = new Event { Name = "Test Event" };

        // Act
        var result = await controller.Create(newEvent) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName);
        mockService.Verify(s => s.CreateEventAsync(newEvent), Times.Once);
    }
    [Fact]
    public async Task Create_Post_ReturnsView_WhenModelStateIsInvalid()
    {
        // Arrange
        var mockService = new Mock<IEventService>();
        var controller = new EventController(mockService.Object);

        controller.ModelState.AddModelError("Name", "Required");

        var newEvent = new Event();

        // Act
        var result = await controller.Create(newEvent) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newEvent, result.Model);
        mockService.Verify(s => s.CreateEventAsync(It.IsAny<Event>()), Times.Never);
    }


}
