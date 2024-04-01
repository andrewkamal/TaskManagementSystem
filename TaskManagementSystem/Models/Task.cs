using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementSystem.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        public Status Status { get; set; }
        public string Assignedby { get; set; }
        public string Assignedto { get; set; }
        [ForeignKey("Assignedby")]
        public virtual ApplicationUser AssignedByUser { get; set; }
        [ForeignKey("Assignedto")]
        public virtual ApplicationUser AssignedToUser { get; set; }
        public List<Comment> Comments { get; set; }
        
    }
}
