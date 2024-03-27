using taskmanagementapp.Models;
using Task = System.Threading.Tasks.Task;

namespace taskmanagementapp.Services
{
    public interface ITaskService
    {
        Task<Models.Task> GetTaskByIdAsync(int id);
        Task AddTaskAsync(TaskDto taskDto);
        Task UpdateTaskAsync(int id, TaskDto taskDto);
        Task DeleteTaskAsync(int id);

        Task AddImageToTaskAsync(int taskId, string imageUrl);

        Task MoveTaskToColumnAsync(int taskId, int columnId);
    }
}
