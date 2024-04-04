using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementSystem.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        [ForeignKey("AssignedByUserId")]
        public string AssignedByUserId { get; set; }
        [ForeignKey("AssignedToUserId")]
        public string AssignedToUserId { get; set; }
        public ApplicationUser AssignedByUser { get; set; }
        public ApplicationUser AssignedToUser { get; set; }
        public List<Comment> Comments { get; set; }
        
    }
}
