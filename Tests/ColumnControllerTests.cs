using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskmanagementapp.Controllers;
using taskmanagementapp.Exceptions;
using taskmanagementapp.Models;
using taskmanagementapp.Services;
using Task = System.Threading.Tasks.Task;

namespace Tests
{
    public class ColumnControllerTests
    {
        [Fact]
        public async Task AddColumn_Returns_OkResult()
        {
            // Arrange
            var mockColumnService = new Mock<IColumnService>();
            var controller = new ColumnController(mockColumnService.Object);
            var columnDto = new ColumnDto();

            // Act
            var result = await controller.AddColumn(columnDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateColumn_Returns_OkResult()
        {
            // Arrange
            var mockColumnService = new Mock<IColumnService>();
            var controller = new ColumnController(mockColumnService.Object);
            var columnDto = new ColumnDto();

            // Act
            var result = await controller.UpdateColumn(1, columnDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateColumn_Returns_NotFoundResult_When_ColumnNotFound()
        {
            // Arrange
            var mockColumnService = new Mock<IColumnService>();
            mockColumnService.Setup(service => service.UpdateColumnAsync(It.IsAny<int>(), It.IsAny<ColumnDto>()))
                              .ThrowsAsync(new NotFoundException("Column not found."));
            var controller = new ColumnController(mockColumnService.Object);
            var columnDto = new ColumnDto();

            // Act
            var result = await controller.UpdateColumn(1, columnDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Column not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteColumn_Returns_OkResult()
        {
            // Arrange
            var mockColumnService = new Mock<IColumnService>();
            var controller = new ColumnController(mockColumnService.Object);

            // Act
            var result = await controller.DeleteColumn(1);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteColumn_Returns_NotFoundResult_When_ColumnNotFound()
        {
            // Arrange
            var mockColumnService = new Mock<IColumnService>();
            mockColumnService.Setup(service => service.DeleteColumnAsync(It.IsAny<int>()))
                              .ThrowsAsync(new NotFoundException("Column not found."));
            var controller = new ColumnController(mockColumnService.Object);

            // Act
            var result = await controller.DeleteColumn(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Column not found.", notFoundResult.Value);
        }
    }
}
