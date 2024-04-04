using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models;
using TaskManagementSystem.ViewModels;

namespace TaskManagementSystem.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<TasksController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITeamRepository _teamRepository;
        private readonly IUserTeamRepository _userTeamRepository;

        public TasksController(RoleManager<IdentityRole> roleManager, ILogger<TasksController> logger, UserManager<ApplicationUser> userManager, ICommentRepository commentRepository, ITaskRepository taskRepository, ITeamRepository teamRepository, IUserTeamRepository userTeamRepository)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;
            _taskRepository = taskRepository;
            _commentRepository = commentRepository;
            _teamRepository = teamRepository;
            _userTeamRepository = userTeamRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            if (id != null)
            {

                var user = await _userManager.FindByIdAsync(id);
                var tasks = _taskRepository.GetTasksAssignedToUser(user.Id);
                _logger.LogWarning($"User {user.Id} viewed tasks assigned to them");
                return View(tasks);
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                var tasks = _taskRepository.GetTasksAssignedToUser(user.Id);
                _logger.LogWarning($"User {user.Id} viewed tasks assigned to them");
                return View(tasks);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        { 
            ViewBag.Users = await _userManager.Users.ToListAsync();
            _logger.LogWarning("Create task page viewed");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskDTO model)
        {
            ViewBag.Users = await _userManager.Users.ToListAsync();
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                var task = new Tasks
                {
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = model.StartDate,
                    DueDate = model.DueDate,
                    Priority = model.Priority,
                    Status = model.Status,
                    AssignedByUserId = model.AssignedByUserId,
                    AssignedToUserId = model.AssignedToUserId,
                };
                var addedTask = _taskRepository.AddTask(task);
                _logger.LogWarning($"Task {addedTask.Id} created by {user.Id}");
                if(model.Comments == null)
                {
                    return RedirectToAction("Index", "Tasks");
                }
                foreach(var comment in model.Comments)
                {
                    var newComment = new Comment
                    {
                        TaskId = task.Id,
                        Text = comment,
                        Date = DateTime.Now,
                        UserId = user.Id
                    };
                    var addedComment = _commentRepository.AddComment(newComment);
                    _logger.LogWarning($"Comment {addedComment.Id} added to task {task.Id} by {user.Id}");
                }
                return RedirectToAction("Index","Tasks");
            }
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                _logger.LogError(error.ErrorMessage);
                ModelState.AddModelError("", error.ErrorMessage);
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = _taskRepository.GetTask(id);
            ViewBag.Users = await _userManager.Users.ToListAsync();
            var model = new EditTaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                StartDate = task.StartDate,
                DueDate = task.DueDate,
                Priority = task.Priority,
                Status = task.Status,
                AssignedByUserId = task.AssignedByUserId,
                AssignedToUserId = task.AssignedToUserId,
                Comments = _commentRepository.GetAllComments().Where(c => c.TaskId == task.Id).Select(t => t.Text).ToList()
            };
            _logger.LogWarning($"Edit task page viewed");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditTaskDTO model)
        {
            ViewBag.Users = await _userManager.Users.ToListAsync();
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                var task = _taskRepository.GetTask(model.Id);
                task.Title = model.Title;
                task.Description = model.Description;
                task.StartDate = model.StartDate;
                task.DueDate = model.DueDate;
                task.Priority = model.Priority;
                task.Status = model.Status;
                task.AssignedByUserId = model.AssignedByUserId;
                task.AssignedToUserId = model.AssignedToUserId;
                var updatedTask = _taskRepository.UpdateTask(task);
                _logger.LogWarning($"Task {updatedTask.Id} updated by {user.Id}");
                var comments = _commentRepository.GetCommentsForTask(updatedTask.Id);
                foreach (var comment in comments)
                {
                    _commentRepository.DeleteComment(comment.Id);
                    _logger.LogWarning($"Comment {comment.Id} deleted by {user.Id}");
                }
                if(model.Comments == null)
                {
                    return RedirectToAction("Index", "Tasks");
                }
                foreach (var comment in model.Comments)
                {
                    var newComment = new Comment
                    {
                        TaskId = updatedTask.Id,
                        Text = comment,
                        Date = DateTime.Now,
                        UserId = user.Id
                    };
                    var addedComment = _commentRepository.AddComment(newComment);
                    _logger.LogWarning($"Comment {addedComment.Id} added to task {updatedTask.Id} by {user.Id}");
                }
                return RedirectToAction("Index", "Tasks");
            }
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                _logger.LogError(error.ErrorMessage);
                ModelState.AddModelError("", error.ErrorMessage);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var task = _taskRepository.GetTask(id);
            var model = new TaskDetailsDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                StartDate = task.StartDate,
                DueDate = task.DueDate,
                Priority = task.Priority,
                Status = task.Status,
                AssignedByUserId = task.AssignedByUserId,
                AssignedToUserId = task.AssignedToUserId,
                Comments = _commentRepository.GetCommentsForTask(task.Id).ToList()
            };
            ViewBag.AssignedByUser = _userManager.FindByIdAsync(task.AssignedByUserId).Result;
            ViewBag.AssignedToUser = _userManager.FindByIdAsync(task.AssignedToUserId).Result;
            _logger.LogWarning($"Task {task.Id} details viewed");
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles="Admin,Lead")]
        public IActionResult Delete(int id)
        {
            var task = _taskRepository.GetTask(id);
            var comments = _commentRepository.GetCommentsForTask(task.Id);
            foreach (var comment in comments)
            {
                _commentRepository.DeleteComment(comment.Id);
                _logger.LogWarning($"Comment {comment.Id} deleted");
            }
            _taskRepository.DeleteTask(id);
            _logger.LogWarning($"Task {task.Id} deleted");
            return RedirectToAction("Index", "Tasks");
        }
        [HttpGet]
        public async Task<IActionResult> ViewDate(DateTime startDate, DateTime endDate)
        {
            var user = await _userManager.GetUserAsync(User);
            var tasks = _taskRepository.GetTasksAssignedToUser(user.Id);
            if (startDate.ToString() == null || endDate.ToString() == null)
                _logger.LogWarning($"User {user.Id} viewed all tasks");
            else
            {
                tasks = tasks.Where(t => t.StartDate >= startDate && t.DueDate <= endDate).ToList();
                _logger.LogWarning($"User {user.Id} viewed tasks between {startDate} and {endDate}");
            }
            return View(tasks);
        }
        [HttpGet]
        public IActionResult All()
        {
            if(User.IsInRole("Admin"))
            {
                var tasks = _taskRepository.GetAllTasks();
                return View(tasks);
            }
            else if(User.IsInRole("Lead"))
            {
                var user = _userManager.GetUserAsync(User).Result;
                var teams = _userTeamRepository.GetUserTeams(user.Id);
                var tasks = new List<Tasks>();
                foreach(var team in teams)
                {
                    var TeamUsers = _userTeamRepository.GetTeamUsers(team.TeamId);
                    foreach(var teamUser in TeamUsers)
                    {
                        var userTasks= _taskRepository.GetTasksAssignedToUser(teamUser.UserId);
                        tasks.AddRange(userTasks);
                    }
                }
                tasks = tasks.Distinct().ToList();
                return View(tasks);
            }
            return View();
        }

    }
}
