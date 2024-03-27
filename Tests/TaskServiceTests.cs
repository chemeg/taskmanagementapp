using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskmanagementapp.Exceptions;
using taskmanagementapp.Models;
using taskmanagementapp.Repository;
using taskmanagementapp.Services;
using Task = System.Threading.Tasks.Task;

namespace Tests
{
    public class TaskServiceTests
    {
        [Fact]
        public async Task AddTaskAsync_Adds_New_Task()
        {
            // Arrange
            var mockTaskRepository = new Mock<ITaskRepository>();
            var service = new TaskService(mockTaskRepository.Object);
            var taskDto = new TaskDto { Name = "New Task", Description = "Description", Deadline = DateTime.Now };

            // Act
            await service.AddTaskAsync(taskDto);

            // Assert
            mockTaskRepository.Verify(repo => repo.AddAsync(It.IsAny<taskmanagementapp.Models.Task>()), Times.Once);
        }

        [Fact]
        public async Task GetTaskByIdAsync_Returns_Task_When_TaskExists()
        {
            // Arrange
            var mockTaskRepository = new Mock<ITaskRepository>();
            var task = new taskmanagementapp.Models.Task { Id = 1, Name = "Task 1" };
            mockTaskRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(task);
            var service = new TaskService(mockTaskRepository.Object);

            // Act
            var result = await service.GetTaskByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Task 1", result.Name);
        }

        [Fact]
        public async Task UpdateTaskAsync_Updates_Task()
        {
            // Arrange
            var mockTaskRepository = new Mock<ITaskRepository>();
            var existingTask = new taskmanagementapp.Models.Task { Id = 1, Name = "Task 1" };
            mockTaskRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingTask);
            var service = new TaskService(mockTaskRepository.Object);
            var taskDto = new TaskDto { Name = "Updated Task", Description = "Updated Description", Deadline = DateTime.Now };

            // Act
            await service.UpdateTaskAsync(1, taskDto);

            // Assert
            mockTaskRepository.Verify(repo => repo.UpdateAsync(existingTask), Times.Once);
            Assert.Equal("Updated Task", existingTask.Name);
            Assert.Equal("Updated Description", existingTask.Description);
            Assert.Equal(taskDto.Deadline, existingTask.Deadline);
        }

        [Fact]
        public async Task UpdateTaskAsync_Throws_NotFoundException_When_TaskNotFound()
        {
            // Arrange
            var mockTaskRepository = new Mock<ITaskRepository>();
            mockTaskRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((taskmanagementapp.Models.Task)null);
            var service = new TaskService(mockTaskRepository.Object);
            var taskDto = new TaskDto { Name = "Updated Task", Description = "Updated Description", Deadline = DateTime.Now };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => service.UpdateTaskAsync(1, taskDto));
        }

        [Fact]
        public async Task DeleteTaskAsync_Deletes_Task()
        {
            // Arrange
            var mockTaskRepository = new Mock<ITaskRepository>();
            var existingTask = new taskmanagementapp.Models.Task { Id = 1, Name = "Task 1" };
            mockTaskRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingTask);
            var service = new TaskService(mockTaskRepository.Object);

            // Act
            await service.DeleteTaskAsync(1);

            // Assert
            mockTaskRepository.Verify(repo => repo.DeleteAsync(existingTask), Times.Once);
        }

        [Fact]
        public async Task DeleteTaskAsync_Throws_NotFoundException_When_TaskNotFound()
        {
            // Arrange
            var mockTaskRepository = new Mock<ITaskRepository>();
            mockTaskRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((taskmanagementapp.Models.Task)null);
            var service = new TaskService(mockTaskRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => service.DeleteTaskAsync(1));
        }

        [Fact]
        public async Task AddImageToTaskAsync_Calls_Repository_Method()
        {
            // Arrange
            var mockTaskRepository = new Mock<ITaskRepository>();
            var service = new TaskService(mockTaskRepository.Object);

            // Act
            await service.AddImageToTaskAsync(1, "image-url");

            // Assert
            mockTaskRepository.Verify(repo => repo.AddImageToTaskAsync(1, "image-url"), Times.Once);
        }

        [Fact]
        public async Task MoveTaskToColumnAsync_Calls_Repository_Method()
        {
            // Arrange
            var mockTaskRepository = new Mock<ITaskRepository>();
            var service = new TaskService(mockTaskRepository.Object);

            // Act
            await service.MoveTaskToColumnAsync(1, 2);

            // Assert
            mockTaskRepository.Verify(repo => repo.MoveTaskToColumnAsync(1, 2), Times.Once);
        }
    }
}
