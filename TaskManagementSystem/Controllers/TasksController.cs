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

        public TasksController(RoleManager<IdentityRole> roleManager, ILogger<TasksController> logger, UserManager<ApplicationUser> userManager, ICommentRepository commentRepository, ITaskRepository taskRepository)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;
            _taskRepository = taskRepository;
            _commentRepository = commentRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var tasks = _taskRepository.GetTasksAssignedToUser(user.Id);
            return View(tasks);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Users = await _userManager.Users.ToListAsync();
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
                Comments = _commentRepository.GetCommentsForTask(task.Id).ToList()
            };
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
                foreach (var comment in model.Comments)
                {
                    var newComment = new Comment
                    {
                        TaskId = updatedTask.Id,
                        Text = comment.Text,
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
            return View(model);
        }
        [HttpPost]
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
    }
}
