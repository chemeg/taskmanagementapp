using taskmanagementapp.Exceptions;
using taskmanagementapp.Models;
using taskmanagementapp.Repository;
using Task = System.Threading.Tasks.Task;

namespace taskmanagementapp.Services
{
    public class TaskService: ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task AddTaskAsync(TaskDto taskDto)
        {
            var task = new Models.Task
            {
                Name = taskDto.Name,
                Description = taskDto.Description,
                Deadline = taskDto.Deadline                
            };

            await _taskRepository.AddAsync(task);
        }

        public async Task<Models.Task> GetTaskByIdAsync(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task UpdateTaskAsync(int id, TaskDto taskDto)
        {
            var existingTask = await _taskRepository.GetByIdAsync(id);

            if (existingTask == null)
                throw new NotFoundException("Task not found.");

            existingTask.Name = taskDto.Name;
            existingTask.Description = taskDto.Description;
            existingTask.Deadline = taskDto.Deadline;            

            await _taskRepository.UpdateAsync(existingTask);
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                throw new NotFoundException("Task not found.");

            await _taskRepository.DeleteAsync(task);
        }

        public async Task AddImageToTaskAsync(int taskId, string imageUrl)
        {
            await _taskRepository.AddImageToTaskAsync(taskId, imageUrl);
        }

        public async Task MoveTaskToColumnAsync(int taskId, int columnId)
        {
            await _taskRepository.MoveTaskToColumnAsync(taskId, columnId);
        }
    }
}
