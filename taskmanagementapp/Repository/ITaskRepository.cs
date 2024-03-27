namespace taskmanagementapp.Repository
{
    public interface ITaskRepository
    {
        Task<Models.Task> GetByIdAsync(int id);
        Task AddAsync(Models.Task task);
        Task UpdateAsync(Models.Task task);

        Task DeleteAsync(Models.Task task);

        Task AddImageToTaskAsync(int taskId, string imageUrl);

        Task MoveTaskToColumnAsync(int taskId, int columnId);
    }
}
