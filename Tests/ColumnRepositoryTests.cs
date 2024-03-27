using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskmanagementapp.Data;
using taskmanagementapp.Models;
using taskmanagementapp.Repository;
using Task = System.Threading.Tasks.Task;

namespace Tests
{
    public class ColumnRepositoryTests
    {
        [Fact]
        public async Task GetByIdAsync_Returns_Column_When_ColumnExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TaskManagementTestDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var column = new Column { Id = 1, Name = "Column 1" };
                context.Columns.Add(column);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new ColumnRepository(context);

                // Act
                var result = await repository.GetByIdAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.Id);
            }
        }

        [Fact]
        public async Task AddAsync_Adds_New_Column()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TaskManagementTestDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var repository = new ColumnRepository(context);
                var column = new Column { Name = "New Column" };

                // Act
                await repository.AddAsync(column);

                // Assert
                var result = await context.Columns.FirstOrDefaultAsync(c => c.Name == "New Column");
                Assert.NotNull(result);
                Assert.Equal("New Column", result.Name);
            }
        }

        [Fact]
        public async Task UpdateAsync_Updates_Column()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TaskManagementTestDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var column = new Column { Id = 1, Name = "Column 1" };
                context.Columns.Add(column);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new ColumnRepository(context);
                var updatedColumn = new Column { Id = 1, Name = "Updated Column" };

                // Act
                await repository.UpdateAsync(updatedColumn);

                // Assert
                var result = await context.Columns.FindAsync(1);
                Assert.NotNull(result);
                Assert.Equal("Updated Column", result.Name);
            }
        }

        [Fact]
        public async Task DeleteAsync_Deletes_Column()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TaskManagementTestDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var column = new Column { Id = 1, Name = "Column 1" };
                context.Columns.Add(column);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new ColumnRepository(context);

                // Act
                var columnToDelete = await context.Columns.FindAsync(1);
                await repository.DeleteAsync(columnToDelete);

                // Assert
                var result = await context.Columns.FindAsync(1);
                Assert.Null(result);
            }
        }
    }
}
