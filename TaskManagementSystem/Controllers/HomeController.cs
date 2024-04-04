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
        private readonly ITeamRepository _teamRepository;
        private readonly IUserTeamRepository _userTeamRepository;

        public HomeController(UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment, ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager, ITeamRepository teamRepository, IUserTeamRepository userTeamRepository)
        {
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _roleManager = roleManager;
            _teamRepository = teamRepository;
            _userTeamRepository = userTeamRepository;
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
                var userTeams = _userTeamRepository.GetUserTeams(user.Id);
                var users = new List<ApplicationUser>();
                foreach (var userT in userTeams)
                {
                    var teamUsers = _userTeamRepository.GetTeamUsers(userT.TeamId);
                    foreach (var teamUser in teamUsers)
                    {
                        if (!users.Contains(teamUser.User))
                            users.Add(teamUser.User);
                    }
                }
                model = users;
            }
            else
                model = new List<ApplicationUser> { user };
            _logger.LogWarning($"User {user.Id} accessed the home page");
            return View(model);
        }
    }
}