using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.ViewModels
{
    public class EditUserDTO
    {
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public List<string> Claims { get; set; } = new List<string>();
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
