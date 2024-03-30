using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.ViewModels
{
    public class RolesDTO
    {
        [Required]
        public string RoleName { get; set; }
    }
}
