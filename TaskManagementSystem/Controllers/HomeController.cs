using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Models;
using TaskManagementSystem.ViewModels;
namespace TaskManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment, ILogger<HomeController> logger)
        {
            _employeeRepository = employeeRepository;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        public IActionResult Index()
        {
            IEnumerable<Employee> model;
            model = _employeeRepository.GetAllEmployees();
            _logger.LogInformation("Index page visited");
            return View(model);
        }
        public IActionResult Details(int ? id)
        {
            Employee employee = _employeeRepository.GetEmployee(id.Value);
            if (employee == null)
            {
                _logger.LogWarning($"Employee with {id.Value} not found");
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }
            _logger.LogInformation($"Employee with {id.Value} found");

            HomeDetailsDTO model = new HomeDetailsDTO();
            model.Employee = employee;
            model.PageTitle = "Employee Details";
            return View(model);
        }
        [HttpGet]
        public IActionResult Create() 
        {           
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeCreateDTO employee)
        {
            if (ModelState.IsValid)
            {
                string FileName = FileProcessing(employee);
                Employee Employee = new Employee
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    Department = employee.Department,
                    PhotoPath = FileName
                };
                _employeeRepository.AddEmployee(Employee);
                return RedirectToAction("details", new { id = Employee.Id });
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            UpdateEmployeeDTO model = new UpdateEmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(UpdateEmployeeDTO employee)
        {
            if (ModelState.IsValid)
            {
                String FileName = employee.ExistingPhotoPath;
                Employee Employee = _employeeRepository.GetEmployee(employee.Id);
                Employee.Name = employee.Name;  
                Employee.Email = employee.Email;
                Employee.Department = employee.Department;
                if (employee.Photo != null)
                {
                    if (employee.ExistingPhotoPath != null)
                    {
                        string FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", employee.ExistingPhotoPath);
                        System.IO.File.Delete(FilePath);
                    }
                    FileName = FileProcessing(employee);
                }
                Employee.PhotoPath = FileName;
                _employeeRepository.UpdateEmployee(Employee);
                return RedirectToAction("index");
            }
            return View();
        }

        private string FileProcessing(EmployeeCreateDTO employee)
        {
            string FileName = string.Empty;
            if (employee.Photo != null)
            {
                string UploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                FileName = Guid.NewGuid().ToString() + "_" + employee.Photo.FileName;
                string FilePath = Path.Combine(UploadsFolder, FileName);
                using (var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    employee.Photo.CopyTo(fileStream);
                }
                _logger.LogInformation($"File {FileName} uploaded");
            }
            return FileName;
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            if (employee.PhotoPath != null)
            {
                string FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", employee.PhotoPath);
                System.IO.File.Delete(FilePath);
            }
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id);
            }
            _employeeRepository.DeleteEmployee(id);
            _logger.LogInformation($"Employee with id {id} deleted");
            return RedirectToAction("index");
        }

        public IActionResult Add()
        { 
            return View();
        }
    }
}
