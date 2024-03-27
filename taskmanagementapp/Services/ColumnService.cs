using Microsoft.EntityFrameworkCore.Metadata.Internal;
using taskmanagementapp.Exceptions;
using taskmanagementapp.Models;
using taskmanagementapp.Repository;
using Column = taskmanagementapp.Models.Column;
using Task = System.Threading.Tasks.Task;

namespace taskmanagementapp.Services
{
    public class ColumnService : IColumnService
    {
        private readonly IColumnRepository _columnRepository;

        public ColumnService(IColumnRepository columnRepository)
        {
            _columnRepository = columnRepository;
        }

        public async Task<Column> GetColumnByIdAsync(int id)
        {
            return await _columnRepository.GetByIdAsync(id);
        }

        public async Task AddColumnAsync(ColumnDto columnDto)
        {
            var column = new Column
            {
                Name = columnDto.Name,
                
            };

            await _columnRepository.AddAsync(column);
        }

        public async Task UpdateColumnAsync(int id, ColumnDto columnDto)
        {
            var existingColumn = await _columnRepository.GetByIdAsync(id);

            if (existingColumn == null)
                throw new NotFoundException("Column not found.");

            existingColumn.Name = columnDto.Name;
            

            await _columnRepository.UpdateAsync(existingColumn);
        }

        public async Task DeleteColumnAsync(int id)
        {
            var column = await _columnRepository.GetByIdAsync(id);

            if (column == null)
                throw new NotFoundException("Column not found.");

            await _columnRepository.DeleteAsync(column);
        }        
    }
}
