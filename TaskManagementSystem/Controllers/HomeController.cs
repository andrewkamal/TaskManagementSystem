using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Models;
using TaskManagementSystem.ViewModels;
namespace TaskManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<HomeController> _logger;

        public HomeController(UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment, ILogger<HomeController> logger)
        {
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<ApplicationUser> model;
            model = _userManager.Users;
            _logger.LogInformation("Index page visited");
            return View(model);
        }

        public IActionResult Add()
        { 
            return View();
        }
    }
}