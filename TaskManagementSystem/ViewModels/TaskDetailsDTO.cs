using TaskManagementSystem.Models;

namespace TaskManagementSystem.ViewModels
{
    public class TaskDetailsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public string AssignedByUserId { get; set; }
        public string AssignedToUserId { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
