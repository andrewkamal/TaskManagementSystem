using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.ViewModels
{
    public class TeamDTO
    {
        [Required]
        public string Name { get; set; }
        public List<ApplicationUser> ? Users { get; set; }
    }
}
