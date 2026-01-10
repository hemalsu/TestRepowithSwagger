using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using WebApplication2.Controllers;
using WebApplication2.Interfaces;
using WebApplication2.Model;
using Xunit;


namespace TestProject1
{
    public class UnitTest1
    {
        private readonly Mock<WebApplication2.Interfaces.IWidgetStorage> _mockWidgetStorage;
        private readonly WidgetsController _controller;

        public UnitTest1()
        {

            _mockWidgetStorage = new Mock<IWidgetStorage>();
            _controller = new WidgetsController(_mockWidgetStorage.Object);
        }
        [Fact]
        public void CreateWidget_ReturnsCreatedWidget_With201AndLocation()
        {
            // Arrange
            var widget = new WidgetDTO { Name = "Test Widget" };
            var createdWidget = new WidgetDTO { Id = Guid.NewGuid(), Name = "Test Widget" };

            _mockWidgetStorage.Setup(s => s.Add(widget)).Returns(createdWidget);

            // Act
            var result = _controller.CreateWigdet(widget) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.Equal(nameof(WidgetsController.GetWidgetByID), result.ActionName);
            var returnedWidget = result.Value as WidgetDTO;
            Assert.NotNull(returnedWidget);
            Assert.Equal(createdWidget.Id, returnedWidget.Id);
            Assert.Equal(createdWidget.Name, returnedWidget.Name);
        }

        [Fact]
        public void GetWidgets_ReturnsAllWidgets_With200()
        {
            // Arrange
            var widgets = new List<WidgetDTO>
            {
                new WidgetDTO { Id = Guid.NewGuid(), Name = "Widget1" },
                new WidgetDTO { Id = Guid.NewGuid(), Name = "Widget2" }
            };

            _mockWidgetStorage.Setup(s => s.GetAllWidgets()).Returns(widgets);

            // Act
            var result = _controller.GetAllWidget() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var returnedWidgets = result.Value as List<WidgetDTO>;
            Assert.NotNull(returnedWidgets);
            Assert.Equal(2, returnedWidgets.Count);
        }

        [Fact]
        public void GetWidgetByID_ReturnsWidget_WhenExists()
        {
            // Arrange

            Guid Ival = Guid.NewGuid();
            var widget = new WidgetDTO { Id = Ival, Name = "Existing Widget" };
            _mockWidgetStorage.Setup(s => s.GetWidgetByID(widget.Id)).Returns(widget);

            // Act
            var result = _controller.GetWidgetByID(Ival) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var returnedWidget = result.Value as WidgetDTO;
            Assert.NotNull(returnedWidget);
            Assert.Equal(widget.Id, returnedWidget.Id);
        }

        [Fact]
        public void GetWidgetByID_Returns404_WhenNotExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockWidgetStorage.Setup(s => s.GetWidgetByID(id)).Returns((WidgetDTO?)null);

            // Act
            var result = _controller.GetWidgetByID(id) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Widget not found", result.Value);
        }
    }
}
