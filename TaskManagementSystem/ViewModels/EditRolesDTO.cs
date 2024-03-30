using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.ViewModels
{
    public class EditRolesDTO
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Role name is required")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; } = new List<string>();
    }
}
