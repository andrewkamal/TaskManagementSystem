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
            var loggedUser = _userManager.GetUserAsync(User).Result;
            var users = new List<ApplicationUser>(); ;
            if (User.IsInRole("Admin"))
                users = _userManager.Users.ToList();
            else if (User.IsInRole("Lead"))
            {
                var userTeams = _userTeamRepository.GetUserTeams(loggedUser.Id);
                var teamUsers = new List<ApplicationUser>();
                foreach (var userT in userTeams)
                {
                    var allUsersTeam = _userTeamRepository.GetTeamUsers(userT.TeamId);
                    foreach (var user in allUsersTeam)
                    {
                        teamUsers.Add(_userManager.FindByIdAsync(user.UserId).Result);
                    }
                }
                teamUsers = teamUsers.Distinct().ToList();
                users = teamUsers;
            }
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
            var loggedUser = _userManager.GetUserAsync(User).Result;
            if (User.IsInRole("Admin"))
                ViewBag.Users = _userManager.Users.ToList();
            else if (User.IsInRole("Lead"))
            {
                var userTeams = _userTeamRepository.GetUserTeams(loggedUser.Id);
                var teamUsers = new List<ApplicationUser>();
                foreach (var userT in userTeams)
                {
                    var allUsersTeam = _userTeamRepository.GetTeamUsers(userT.TeamId);
                    foreach (var user in allUsersTeam)
                    {
                        teamUsers.Add(_userManager.FindByIdAsync(user.UserId).Result);
                    }
                }
                teamUsers = teamUsers.Distinct().ToList();
                ViewBag.Users = teamUsers;
            }
            if (id != null)
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
            return View();
        }
        [HttpGet]
        public IActionResult UserTaskCompletion(string id)
        {
            var loggedUser = _userManager.GetUserAsync(User).Result;
            if (User.IsInRole("Admin"))
                ViewBag.Users = _userManager.Users.ToList();
            else if (User.IsInRole("Lead"))
            {
                var userTeams = _userTeamRepository.GetUserTeams(loggedUser.Id);
                var teamUsers = new List<ApplicationUser>();
                foreach (var userT in userTeams)
                {
                    var allUsersTeam = _userTeamRepository.GetTeamUsers(userT.TeamId);
                    foreach (var user in allUsersTeam)
                    {
                        teamUsers.Add(_userManager.FindByIdAsync(user.UserId).Result);
                    }
                }
                teamUsers = teamUsers.Distinct().ToList();
                ViewBag.Users = teamUsers;
            }
            if (id != null)
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
            return View();
        }
        [HttpGet]
        public IActionResult UsersTaskCompletion()
        {
            var loggedUser = _userManager.GetUserAsync(User).Result;
            var users = new List<ApplicationUser>(); ;
            var report = new List<TaskCompletionDTO>();
            if (User.IsInRole("Admin"))
                users = _userManager.Users.ToList();
            else if (User.IsInRole("Lead"))
            {
                var userTeams = _userTeamRepository.GetUserTeams(loggedUser.Id);
                var teamUsers = new List<ApplicationUser>();
                foreach (var userT in userTeams)
                {
                    var allUsersTeam = _userTeamRepository.GetTeamUsers(userT.TeamId);
                    foreach (var user in allUsersTeam)
                    {
                        teamUsers.Add(_userManager.FindByIdAsync(user.UserId).Result);
                    }
                }
                teamUsers = teamUsers.Distinct().ToList();
                users = teamUsers;
            }
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
            var loggedUser = _userManager.GetUserAsync(User).Result;
            var teams = new List<Team>();

            if (User.IsInRole("Admin"))
                ViewBag.Teams = _teamRepository.GetAllTeams().ToList();
            else if (User.IsInRole("Lead"))
            {
                var userTeams = _userTeamRepository.GetUserTeams(loggedUser.Id);
                foreach (var userT in userTeams)
                {
                    teams.Add(_teamRepository.GetTeam(userT.TeamId));
                }
                ViewBag.Teams = teams.Distinct().ToList();
            }
            if(id != 0)
            {
                var users = _userTeamRepository.GetTeamUsers(id);
                var completedTasks = 0;
                var totalTasks = 0;

                foreach (var user in users)
                {
                    var userTasks = _taskRepository.GetTasksAssignedToUser(user.UserId);
                    completedTasks += userTasks.Count(task => task.Status == Status.Completed);
                    totalTasks += userTasks.Count();
                }

                var performance = new TeamPerformanceDTO
                {
                    Team = _teamRepository.GetTeam(id),
                    TotalMembers = users.Count(),
                    Performance = (double)completedTasks / totalTasks
                };
                _logger.LogInformation($"Team Performance Report for teamID {id} was generated");
                return View(performance);
            }
            return View();
        }
        [HttpGet]
        public IActionResult TeamsPerformance()
        {
            var loggedUser = _userManager.GetUserAsync(User).Result;
            var teams = new List<Team>();
            var performance = new List<TeamPerformanceDTO>();
            
            if(User.IsInRole("Admin"))
                teams = _teamRepository.GetAllTeams().ToList();
            else if(User.IsInRole("Lead"))
            {
                var userTeams = _userTeamRepository.GetUserTeams(loggedUser.Id);
                foreach (var userT in userTeams)
                {
                    teams.Add(_teamRepository.GetTeam(userT.TeamId));
                }
                teams = teams.Distinct().ToList();
            }

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

                performance.Add(new TeamPerformanceDTO
                {
                    Team = team,
                    TotalMembers = users.Count(),
                    Performance = (double)completedTasks / totalTasks
                });
            }
            _logger.LogInformation("Teams Performance Report was generated");
            return View(performance);
        }
    }
}
