using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.ViewModels
{
    public class EditUserDTO
    {
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public Dept Department { get; set; }
        public IFormFile? Photo { get; set; }
        public string ExistingPhotoPath { get; set; }
        public List<string> Claims { get; set; } = new List<string>();
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
