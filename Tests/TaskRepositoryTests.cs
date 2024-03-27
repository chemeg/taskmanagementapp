using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskmanagementapp.Data;
using taskmanagementapp.Exceptions;
using taskmanagementapp.Repository;

namespace Tests
{
    public class TaskRepositoryTests
    {
        [Fact]
        public async Task AddAsync_Adds_New_Task()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TaskManagementTestDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var repository = new TaskRepository(context);
                var task = new taskmanagementapp.Models.Task { Name = "New Task" };

                // Act
                await repository.AddAsync(task);

                // Assert
                var result = await context.Tasks.FirstOrDefaultAsync(t => t.Name == "New Task");
                Assert.NotNull(result);
                Assert.Equal("New Task", result.Name);
            }
        }

        [Fact]
        public async Task GetByIdAsync_Returns_Task_When_TaskExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TaskManagementTestDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var task = new taskmanagementapp.Models.Task { Id = 1, Name = "Task 1" };
                context.Tasks.Add(task);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new TaskRepository(context);

                // Act
                var result = await repository.GetByIdAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.Id);
                Assert.Equal("Task 1", result.Name);
            }
        }

        [Fact]
        public async Task UpdateAsync_Updates_Task()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TaskManagementTestDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var task = new taskmanagementapp.Models.Task { Id = 1, Name = "Task 1" };
                context.Tasks.Add(task);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new TaskRepository(context);
                var updatedTask = new taskmanagementapp.Models.Task { Id = 1, Name = "Updated Task" };

                // Act
                await repository.UpdateAsync(updatedTask);

                // Assert
                var result = await context.Tasks.FindAsync(1);
                Assert.NotNull(result);
                Assert.Equal("Updated Task", result.Name);
            }
        }

        [Fact]
        public async Task DeleteAsync_Deletes_Task()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TaskManagementTestDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var task = new taskmanagementapp.Models.Task { Id = 1, Name = "Task 1" };
                context.Tasks.Add(task);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new TaskRepository(context);

                // Act
                var taskToDelete = await context.Tasks.FindAsync(1);
                await repository.DeleteAsync(taskToDelete);

                // Assert
                var result = await context.Tasks.FindAsync(1);
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task AddImageToTaskAsync_Adds_ImageUrl_To_Task()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TaskManagementTestDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var task = new taskmanagementapp.Models.Task { Id = 1, Name = "Task 1" };
                context.Tasks.Add(task);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new TaskRepository(context);

                // Act
                await repository.AddImageToTaskAsync(1, "image-url");

                // Assert
                var task = await context.Tasks.FindAsync(1);
                Assert.NotNull(task);
                Assert.Single(task.ImageUrls);                
            }
        }

        [Fact]
        public async Task AddImageToTaskAsync_Throws_NotFoundException_When_TaskNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TaskManagementTestDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var repository = new TaskRepository(context);

                // Act & Assert
                await Assert.ThrowsAsync<NotFoundException>(() => repository.AddImageToTaskAsync(1, "image-url"));
            }
        }
    }
}
