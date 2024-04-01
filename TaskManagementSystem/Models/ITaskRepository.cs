namespace TaskManagementSystem.Models
{
    public interface ITaskRepository
    {
        Task GetTask(int id);
        IEnumerable<Task> GetAllTasks();
        Task AddTask(Task task);
        Task UpdateTask(Task taskChanges);
        Task DeleteTask(int id);
    }
}
