using Microsoft.EntityFrameworkCore.Metadata.Internal;
using taskmanagementapp.Models;
using Column = taskmanagementapp.Models.Column;
using Task = System.Threading.Tasks.Task;

namespace taskmanagementapp.Services
{
    public interface IColumnService
    {
        Task<Column> GetColumnByIdAsync(int id);
        Task AddColumnAsync(ColumnDto columnDto);
        Task UpdateColumnAsync(int id, ColumnDto columnDto);
        Task DeleteColumnAsync(int id);
    }
}
