using taskmanagementapp.Data;
using taskmanagementapp.Exceptions;

namespace taskmanagementapp.Repository
{
    public class TaskRepository: ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Models.Task task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task<Models.Task> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task UpdateAsync(Models.Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Models.Task task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task AddImageToTaskAsync(int taskId, string imageUrl)
        {
            var task = await GetByIdAsync(taskId);
            if (task == null)
                throw new NotFoundException("Task not found.");

            task.ImageUrls.Add(imageUrl);
            await _context.SaveChangesAsync();
        }

        public async Task MoveTaskToColumnAsync(int taskId, int columnId)
        {
            var task = await GetByIdAsync(taskId);
            if (task == null)
                throw new NotFoundException("Task not found.");
            
            task.ColumnId = columnId; 
            await _context.SaveChangesAsync();
        }        
    }
}
