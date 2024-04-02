using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models;
using TaskManagementSystem.ViewModels;
namespace TaskManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<HomeController> _logger;

        public HomeController(UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment, ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<ApplicationUser> model;
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Admin"))
                model = await _userManager.Users.ToListAsync();
            else if (roles.Contains("Lead"))
            {
                var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
                var allUsers = await _userManager.Users.ToListAsync();
                model = allUsers.Except(adminUsers).ToList();
            }
            else
                model = new List<ApplicationUser> { user };
            _logger.LogWarning($"User {user.Id} accessed the home page");
            return View(model);
        }
    }
}