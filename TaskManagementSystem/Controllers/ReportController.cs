using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Models;
using TaskManagementSystem.ViewModels;

namespace TaskManagementSystem.Controllers
{
    [Authorize(Roles = "Admin,Lead")]
    public class ReportController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<ReportController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITeamRepository _teamRepository;
        private readonly IUserTeamRepository _userTeamRepository;
        public ReportController(RoleManager<IdentityRole> roleManager, ILogger<ReportController> logger, UserManager<ApplicationUser> userManager, ICommentRepository commentRepository, ITaskRepository taskRepository, IUserTeamRepository userTeamRepository, ITeamRepository teamRepository)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;
            _taskRepository = taskRepository;
            _commentRepository = commentRepository;
            _userTeamRepository = userTeamRepository;
            _teamRepository = teamRepository;
        }
        [HttpGet]
        public IActionResult UsersProductivity()
        {
            var users = _userManager.Users.ToList();
            var report = new List<UserProductivityDTO>();

            foreach (var user in users)
            {
                var userTasks = _taskRepository.GetTasksAssignedToUser(user.Id);
                var completedTasks = userTasks.Count(task => task.Status == Status.Completed);
                var productivity = (double)completedTasks / userTasks.Count();

                report.Add(new UserProductivityDTO
                {
                    User = user,
                    Productivity = productivity
                });
            }
            _logger.LogInformation("Users Productivity Report was generated");
            return View(report);
        }
        [HttpGet]
        public IActionResult UserProductivity(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            var userTasks = _taskRepository.GetTasksAssignedToUser(user.Id);
            var completedTasks = userTasks.Count(task => task.Status == Status.Completed);
            var productivity = (double)completedTasks / userTasks.Count();

            var report = new UserProductivityDTO
            {
                User = user,
                Productivity = productivity
            };
            _logger.LogInformation($"User Productivity Report for user {user.UserName} was generated");
            return View(report);
        }
        [HttpGet]
        public IActionResult UserTaskCompletion(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            var userTasks = _taskRepository.GetTasksAssignedToUser(user.Id);
            var completedTasks = userTasks.Count(task => task.Status == Status.Completed);
            var incompletedTasks = userTasks.Count(task => task.Status != Status.Completed);

            var report = new TaskCompletionDTO
            {
                User = user,
                CompletedTasks = completedTasks,
                IncompletedTasks = incompletedTasks,
                TotalTasks = userTasks.Count()
            };
            _logger.LogInformation($"User Task Completion Report for user {user.UserName} was generated");
            return View(report);
        }
        [HttpGet]
        public IActionResult UsersTaskCompletion()
        {
            var users = _userManager.Users.ToList();
            var report = new List<TaskCompletionDTO>();

            foreach (var user in users)
            {
                var userTasks = _taskRepository.GetTasksAssignedToUser(user.Id);
                var completedTasks = userTasks.Count(task => task.Status == Status.Completed);
                var incompletedTasks = userTasks.Count(task => task.Status != Status.Completed);

                report.Add(new TaskCompletionDTO
                {
                    User = user,
                    CompletedTasks = completedTasks,
                    IncompletedTasks = incompletedTasks,
                    TotalTasks = userTasks.Count()
                });
            }
            _logger.LogInformation("Users Task Completion Report was generated");
            return View(report);
        }
        [HttpGet]
        public IActionResult TeamPerformance(int id)
        {
            var team = _teamRepository.GetTeam(id);
            var users = _userTeamRepository.GetTeamUsers(id);
            var completedTasks = 0;
            var totalTasks = 0;

            foreach (var user in users)
            {
                var userTasks = _taskRepository.GetTasksAssignedToUser(user.UserId);
                completedTasks += userTasks.Count(task => task.Status == Status.Completed);
                totalTasks += userTasks.Count();
            }

            var report = new TeamPerformanceDTO
            {
                TeamName = team.Name,
                Performance = (double)completedTasks / totalTasks
            };

            _logger.LogInformation($"Team Performance Report for team {team.Name} was generated");
            return View(report);
        }
        [HttpGet]
        public IActionResult TeamsPerformance()
        {
            var teams = _teamRepository.GetAllTeams();
            var report = new List<TeamPerformanceDTO>();

            foreach (var team in teams)
            {
                var users = _userTeamRepository.GetTeamUsers(team.Id);
                var completedTasks = 0;
                var totalTasks = 0;

                foreach (var user in users)
                {
                    var userTasks = _taskRepository.GetTasksAssignedToUser(user.UserId);
                    completedTasks += userTasks.Count(task => task.Status == Status.Completed);
                    totalTasks += userTasks.Count();
                }

                report.Add(new TeamPerformanceDTO
                {
                    TeamName = team.Name,
                    Performance = (double)completedTasks / totalTasks
                });
            }
            _logger.LogInformation("Teams Performance Report was generated");
            return View(report);
        }
    }
}
