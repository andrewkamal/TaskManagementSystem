using TaskManagementSystem.Models;

namespace TaskManagementSystem.ViewModels
{
    public class TaskCompletionDTO
    {
        public ApplicationUser User { get; set; }
        public int CompletedTasks { get; set; }
        public int IncompletedTasks { get; set; }
        public int TotalTasks { get; set; }
    }
}
