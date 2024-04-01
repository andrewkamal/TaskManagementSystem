
namespace TaskManagementSystem.Models
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TMSDbContext DB;
        public TaskRepository(TMSDbContext DB)
        {
            this.DB = DB;
        }

        public Task AddTask(Task task)
        {
            DB.Task.Add(task);
            DB.SaveChanges();
            return task;
        }

        public Task DeleteTask(int id)
        {
            Task task = DB.Task.Find(id);
            if (task != null)
            {
                DB.Task.Remove(task);
                DB.SaveChanges();
            }
            return task;
        }

        public IEnumerable<Task> GetAllTasks()
        {
            return DB.Task;
        }

        public Task GetTask(int id)
        {
            return DB.Task.Find(id);
        }

        public Task UpdateTask(Task taskChanges)
        {
            var task = DB.Task.Attach(taskChanges);
            task.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            DB.SaveChanges();
            return taskChanges;
        }
    }
}
