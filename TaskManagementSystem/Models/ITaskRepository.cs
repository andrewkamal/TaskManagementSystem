namespace TaskManagementSystem.Models
{
    public interface ITaskRepository
    {
        Tasks GetTask(int id);
        IEnumerable<Tasks> GetTasksAssignedToUser(string userId);
        IEnumerable<Tasks> GetTasksFiltered(Priority? priority, Status? status, string userId);
        IEnumerable<Tasks> GetAllTasks();
        Tasks AddTask(Tasks task);
        Tasks UpdateTask(Tasks taskChanges);
        Tasks DeleteTask(int id);
    }
}
