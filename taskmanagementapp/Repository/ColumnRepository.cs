using taskmanagementapp.Data;
using taskmanagementapp.Models;
using Task = System.Threading.Tasks.Task;

namespace taskmanagementapp.Repository
{
    public class ColumnRepository : IColumnRepository
    {
        private readonly AppDbContext _context;

        public ColumnRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Column> GetByIdAsync(int id)
        {
            return await _context.Columns.FindAsync(id);
        }

        public async Task AddAsync(Column column)
        {
            await _context.Columns.AddAsync(column);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Column column)
        {
            _context.Columns.Update(column);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Column column)
        {
            _context.Columns.Remove(column);
            await _context.SaveChangesAsync();
        }
       
    }
}
