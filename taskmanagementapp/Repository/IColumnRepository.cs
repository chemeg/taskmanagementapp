using taskmanagementapp.Models;
using Task = System.Threading.Tasks.Task;

namespace taskmanagementapp.Repository
{
    public interface IColumnRepository
    {
        Task<Column> GetByIdAsync(int id);
        Task AddAsync(Column column);
        Task UpdateAsync(Column column);
        Task DeleteAsync(Column column);
    }
}
