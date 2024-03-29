namespace TaskManagementSystem.ViewModels
{
    public class UpdateEmployeeDTO : EmployeeCreateDTO
    {
        public int Id { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
