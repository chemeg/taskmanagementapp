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
    public class ColumnServiceTests
    {
        [Fact]
        public async Task GetColumnByIdAsync_Returns_Column_When_ColumnExists()
        {
            // Arrange
            var mockColumnRepository = new Mock<IColumnRepository>();
            var column = new Column { Id = 1, Name = "Column 1" };
            mockColumnRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(column);
            var service = new ColumnService(mockColumnRepository.Object);

            // Act
            var result = await service.GetColumnByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Column 1", result.Name);
        }

        [Fact]
        public async Task AddColumnAsync_Adds_New_Column()
        {
            // Arrange
            var mockColumnRepository = new Mock<IColumnRepository>();
            var service = new ColumnService(mockColumnRepository.Object);
            var columnDto = new ColumnDto { Name = "New Column" };

            // Act
            await service.AddColumnAsync(columnDto);

            // Assert
            mockColumnRepository.Verify(repo => repo.AddAsync(It.IsAny<Column>()), Times.Once);
        }

        [Fact]
        public async Task UpdateColumnAsync_Updates_Column()
        {
            // Arrange
            var mockColumnRepository = new Mock<IColumnRepository>();
            var existingColumn = new Column { Id = 1, Name = "Column 1" };
            mockColumnRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingColumn);
            var service = new ColumnService(mockColumnRepository.Object);
            var columnDto = new ColumnDto { Name = "Updated Column" };

            // Act
            await service.UpdateColumnAsync(1, columnDto);

            // Assert
            mockColumnRepository.Verify(repo => repo.UpdateAsync(existingColumn), Times.Once);
            Assert.Equal("Updated Column", existingColumn.Name);
        }

        [Fact]
        public async Task UpdateColumnAsync_Throws_NotFoundException_When_ColumnNotFound()
        {
            // Arrange
            var mockColumnRepository = new Mock<IColumnRepository>();
            mockColumnRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Column)null);
            var service = new ColumnService(mockColumnRepository.Object);
            var columnDto = new ColumnDto { Name = "Updated Column" };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => service.UpdateColumnAsync(1, columnDto));
        }

        [Fact]
        public async Task DeleteColumnAsync_Deletes_Column()
        {
            // Arrange
            var mockColumnRepository = new Mock<IColumnRepository>();
            var existingColumn = new Column { Id = 1, Name = "Column 1" };
            mockColumnRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingColumn);
            var service = new ColumnService(mockColumnRepository.Object);

            // Act
            await service.DeleteColumnAsync(1);

            // Assert
            mockColumnRepository.Verify(repo => repo.DeleteAsync(existingColumn), Times.Once);
        }

        [Fact]
        public async Task DeleteColumnAsync_Throws_NotFoundException_When_ColumnNotFound()
        {
            // Arrange
            var mockColumnRepository = new Mock<IColumnRepository>();
            mockColumnRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Column)null);
            var service = new ColumnService(mockColumnRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => service.DeleteColumnAsync(1));
        }
    }
}
