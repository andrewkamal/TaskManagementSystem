
using Microsoft.EntityFrameworkCore;

namespace TaskManagementSystem.Models
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TMSDbContext DB;
        public TaskRepository(TMSDbContext DB)
        {
            this.DB = DB;
        }

        public Tasks AddTask(Tasks task)
        {
            DB.Task.Add(task);
            DB.SaveChanges();
            return task;
        }
        public Tasks DeleteTask(int id)
        {
            Tasks task = DB.Task.Find(id);
            if (task != null)
            {
                DB.Task.Remove(task);
                DB.SaveChanges();
            }
            return task;
        }

        public IEnumerable<Tasks> GetAllTasks()
        {
            return DB.Task;
        }

        public Tasks GetTask(int id)
        {
            return DB.Task.Find(id);
        }

        public IEnumerable<Tasks> GetTasksAssignedToUser(string userId)
        {
            var tasks = DB.Task
                .Include(t => t.AssignedByUser)
                .Where(t => t.AssignedToUserId == userId)
                .ToList();
            return tasks;
        }

        public IEnumerable<Tasks> GetTasksFiltered(Priority? priority, Status? status, string userId)
        {
            if(priority == null && status == null)
            {
                return DB.Task.Where(t => t.AssignedToUserId == userId);
            }
            else if(priority == null)
            {
                return DB.Task.Where(t => t.Status == status && t.AssignedToUserId == userId);
            }
            else if(status == null)
            {
                return DB.Task.Where(t => t.Priority == priority && t.AssignedToUserId == userId);
            }
            return DB.Task.Where(t => t.Priority == priority && t.Status == status && t.AssignedToUserId == userId);
        }

        public Tasks UpdateTask(Tasks taskChanges)
        {
            var task = DB.Task.Attach(taskChanges);
            task.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            DB.SaveChanges();
            return taskChanges;
        }
    }
}
