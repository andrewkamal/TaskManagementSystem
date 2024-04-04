using TaskManagementSystem.Models;

namespace TaskManagementSystem.ViewModels
{
    public class UserProductivityDTO
    {
        public ApplicationUser User { get; set; }
        public double Productivity { get; set; }
    }
}
