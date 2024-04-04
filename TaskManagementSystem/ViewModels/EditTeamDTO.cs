using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.ViewModels
{
    public class EditTeamDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Team name is required")]
        public string TeamName { get; set; }
        public List<string> Users { get; set; } = new List<string>();
    }
}
