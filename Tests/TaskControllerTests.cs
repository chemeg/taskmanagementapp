using Microsoft.AspNetCore.Mvc;
using Moq;
using taskmanagementapp.Controllers;
using taskmanagementapp.Exceptions;
using taskmanagementapp.Models;
using taskmanagementapp.Services;
using Task = System.Threading.Tasks.Task;

namespace Tests
{
    public class TaskControllerTests
    {

        [Fact]
        public async Task AddTask_Returns_OkResult()
        {
            // Arrange
            var mockTaskService = new Mock<ITaskService>();
            var controller = new TaskController(mockTaskService.Object);
            var taskDto = new TaskDto();

            // Act
            var result = await controller.AddTask(taskDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateTask_Returns_OkResult()
        {
            // Arrange
            var mockTaskService = new Mock<ITaskService>();
            var controller = new TaskController(mockTaskService.Object);
            var taskDto = new TaskDto();

            // Act
            var result = await controller.UpdateTask(1, taskDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateTask_Returns_NotFoundResult_When_TaskNotFound()
        {
            // Arrange
            var mockTaskService = new Mock<ITaskService>();
            mockTaskService.Setup(service => service.UpdateTaskAsync(It.IsAny<int>(), It.IsAny<TaskDto>()))
                            .ThrowsAsync(new NotFoundException("Task not found."));
            var controller = new TaskController(mockTaskService.Object);
            var taskDto = new TaskDto();

            // Act
            var result = await controller.UpdateTask(1, taskDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Task not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteTask_Returns_OkResult()
        {
            // Arrange
            var mockTaskService = new Mock<ITaskService>();
            var controller = new TaskController(mockTaskService.Object);

            // Act
            var result = await controller.DeleteTask(1);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteTask_Returns_NotFoundResult_When_TaskNotFound()
        {
            // Arrange
            var mockTaskService = new Mock<ITaskService>();
            mockTaskService.Setup(service => service.DeleteTaskAsync(It.IsAny<int>()))
                            .ThrowsAsync(new NotFoundException("Task not found."));
            var controller = new TaskController(mockTaskService.Object);

            // Act
            var result = await controller.DeleteTask(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Task not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task AddImageToTask_Returns_OkResult()
        {
            // Arrange
            var mockTaskService = new Mock<ITaskService>();
            var controller = new TaskController(mockTaskService.Object);

            // Act
            var result = await controller.AddImageToTask(1, "image-url");

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task AddImageToTask_Returns_NotFoundResult_When_TaskNotFound()
        {
            // Arrange
            var mockTaskService = new Mock<ITaskService>();
            mockTaskService.Setup(service => service.AddImageToTaskAsync(It.IsAny<int>(), It.IsAny<string>()))
                            .ThrowsAsync(new NotFoundException("Task not found."));
            var controller = new TaskController(mockTaskService.Object);

            // Act
            var result = await controller.AddImageToTask(1, "image-url");

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Task not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetTaskById_Returns_OkResult_With_Task()
        {
            // Arrange
            var mockTaskService = new Mock<ITaskService>();
            var task = new taskmanagementapp.Models.Task { Id = 1, Name = "Task 1" };
            mockTaskService.Setup(service => service.GetTaskByIdAsync(1)).ReturnsAsync(task);
            var controller = new TaskController(mockTaskService.Object);

            // Act
            var result = await controller.GetTaskById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTask = Assert.IsType<taskmanagementapp.Models.Task>(okResult.Value);
            Assert.Equal(task.Id, returnedTask.Id);
            Assert.Equal(task.Name, returnedTask.Name);
        }

        [Fact]
        public async Task GetTaskById_Returns_NotFoundResult_When_TaskNotFound()
        {
            // Arrange
            var mockTaskService = new Mock<ITaskService>();
            mockTaskService.Setup(service => service.GetTaskByIdAsync(It.IsAny<int>())).ReturnsAsync((taskmanagementapp.Models.Task)null);
            var controller = new TaskController(mockTaskService.Object);

            // Act
            var result = await controller.GetTaskById(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task MoveTaskToColumn_Returns_OkResult()
        {
            // Arrange
            var mockTaskService = new Mock<ITaskService>();
            var controller = new TaskController(mockTaskService.Object);

            // Act
            var result = await controller.MoveTaskToColumn(1, 2);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }
    }
}
