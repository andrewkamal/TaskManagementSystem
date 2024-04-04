using TaskManagementSystem.Models;

namespace TaskManagementSystem.ViewModels
{
    public class TeamPerformanceDTO
    {
        public Team Team { get; set; }
        public int TotalMembers { get; set; }
        public double Performance { get; set; }
    }
}
