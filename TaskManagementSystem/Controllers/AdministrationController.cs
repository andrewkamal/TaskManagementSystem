using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Models;
using TaskManagementSystem.ViewModels;

namespace TaskManagementSystem.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AdministrationController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ITeamRepository _teamRepository;
        private readonly IUserTeamRepository _userTeamRepository;
        public AdministrationController(RoleManager<IdentityRole> roleManager, ILogger<AdministrationController> logger, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment, ITeamRepository teamRepository, IUserTeamRepository userTeamRepository)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _teamRepository = teamRepository;
            _userTeamRepository = userTeamRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            var model = new List<UserRolesDTO>();
            foreach (var role in _roleManager.Roles)
            {
                var userRolesDTO = new UserRolesDTO
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesDTO.IsSelected = true;
                }
                else
                {
                    userRolesDTO.IsSelected = false;
                }
                model.Add(userRolesDTO);
            }
            _logger.LogWarning($"Roles of user with Id = {userId} listed");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesDTO> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.IsSelected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            _logger.LogWarning($"Roles of user with Id = {userId} updated");
            return RedirectToAction("EditUser", new { Id = userId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            string FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", user.PhotoPath);
            if (System.IO.File.Exists(FilePath))
            {
                System.IO.File.Delete(FilePath);
                _logger.LogWarning($"File {user.PhotoPath} deleted");
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("ListUsers");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            _logger.LogWarning($"User with Id = {id} deleted");
            return View("ListUsers");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            _logger.LogWarning($"Role with Id = {id} deleted");
            return View("ListRoles");
        }

        [HttpGet]
        public IActionResult ListUsers(string search)
        {
            var users = _userManager.Users;
            if (!String.IsNullOrEmpty(search))
            {
                users = users.Where(u => u.UserName.Contains(search));
            }
            _logger.LogWarning("All Users listed");
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            var model = new EditUserDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Name = user.Name,
                Department = user.Department,
                ExistingPhotoPath = user.PhotoPath,
                Roles = userRoles,
                Claims = userClaims.Select(c => c.Value).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserDTO model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                if (model.Photo != null)
                {
                    string FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", user.PhotoPath);
                    if (System.IO.File.Exists(FilePath))
                    {
                        System.IO.File.Delete(FilePath);
                        _logger.LogWarning($"File {user.PhotoPath} deleted");
                    }
                }
                string FileName = FileProcessing(model);
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Name = model.Name;
                user.Department = model.Department;
                user.PhotoPath = FileName;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                _logger.LogWarning($"User with Id = {model.Id} updated");
                return View(model);
            }
        }

        private string FileProcessing(EditUserDTO model)
        {
            string FileName = string.Empty;
            if (model.Photo != null)
            {
                string UploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                FileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string FilePath = Path.Combine(UploadsFolder, FileName);
                using (var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
                _logger.LogWarning($"File {FileName} uploaded");
            }
            return FileName;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RolesDTO model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    _logger.LogWarning($"Role {model.RoleName} created successfully");
                    return RedirectToAction("ListRoles", "Administration");
                }
                foreach (IdentityError error in result.Errors)
                {
                    _logger.LogError($"Error while creating role {model.RoleName}");
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ListRoles(string search)
        {
            var roles = _roleManager.Roles;
            if (!String.IsNullOrEmpty(search))
            {
                roles = roles.Where(r => r.Name.Contains(search));
            }
            _logger.LogWarning("All Roles listed");
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            var model = new EditRolesDTO
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                    model.Users.Add(user.UserName);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRolesDTO model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                _logger.LogWarning($"Role {model.RoleName} updated");
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }
            var model = new List<UserRoleDTO>();

            foreach (var user in _userManager.Users)
            {
                var userRoleDTO = new UserRoleDTO
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleDTO.IsSelected = true;
                }
                else
                {
                    userRoleDTO.IsSelected = false;
                }
                model.Add(userRoleDTO);
            }
            _logger.LogWarning($"Users in role {role.Name} listed");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleDTO> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    _logger.LogWarning($"User {user.UserName} added to role {role.Name}");
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    _logger.LogWarning($"User {user.UserName} removed from role {role.Name}");
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                    continue;
            }
            _logger.LogWarning($"Role {role.Name} updated");
            return RedirectToAction("EditRole", new { Id = roleId });
        }
        [HttpGet]
        public IActionResult CreateTeam()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTeam(TeamDTO model)
        {
            if (ModelState.IsValid)
            {
                var team = new Team
                {
                    Name = model.Name
                };
                _teamRepository.AddTeam(team);
                _logger.LogWarning($"Team {model.Name} created");
                return RedirectToAction("ListTeams");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ListTeams(string search)
        {
            var teams = _teamRepository.GetAllTeams();
            if (!String.IsNullOrEmpty(search))
            {
                teams = teams.Where(t => t.Name.Contains(search));
                _logger.LogWarning($"Teams with name containing {search} listed");
            }
            else
                _logger.LogWarning("All Teams listed");
            return View(teams);
        }
        [HttpGet]
        public IActionResult EditTeam(int id)
        {
            var team = _teamRepository.GetTeam(id);
            var model = new EditTeamDTO
            {
                Id = team.Id,
                TeamName = team.Name,
                Users = new List<string>()
            };
            var teamUsers = _userTeamRepository.GetTeamUsers(id);
            foreach (var user in teamUsers)
            {
                model.Users.Add(user.User.UserName);
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult EditTeam(EditTeamDTO model)
        {
            if (ModelState.IsValid)
            {
                var team = new Team
                {
                    Id = model.Id,
                    Name = model.TeamName
                };
                _teamRepository.UpdateTeam(team);
                _logger.LogWarning($"Team with Id = {model.Id} updated");
                return RedirectToAction("ListTeams");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult DeleteTeam(int id)
        {
            _teamRepository.DeleteTeam(id);
            _logger.LogWarning($"Team with Id = {id} deleted");
            return RedirectToAction("ListTeams");
        }
        [HttpGet]
        public IActionResult EditUsersInTeam(int teamId)
        {
            ViewBag.teamId = teamId;
            var team = _teamRepository.GetTeam(teamId);
            var model = new List<UserTeamDTO>();
            foreach (var user in _userManager.Users)
            {
                var userTeamDTO = new UserTeamDTO
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (_userTeamRepository.IsUserInTeam(user.Id, teamId))
                    userTeamDTO.IsSelected = true;
                else
                    userTeamDTO.IsSelected = false;
                model.Add(userTeamDTO);
            }
            _logger.LogWarning($"Users in team {team.Name} listed");
            return View(model);
        }
        [HttpPost]
        public IActionResult EditUsersInTeam(List<UserTeamDTO> model, int teamId)
        {
            var team = _teamRepository.GetTeam(teamId);
            for (int i = 0; i < model.Count; i++)
            {
                var user = _userManager.FindByIdAsync(model[i].UserId).Result;
                if (model[i].IsSelected && !_userTeamRepository.IsUserInTeam(user.Id, teamId))
                {
                    var userTeam = new UserTeam
                    {
                        UserId = user.Id,
                        TeamId = teamId
                    };
                    _logger.LogWarning($"User {user.UserName} added to team {team.Name}");
                    _userTeamRepository.AddUserTeam(userTeam);
                }
                else if (!model[i].IsSelected && _userTeamRepository.IsUserInTeam(user.Id, teamId))
                {
                    _logger.LogWarning($"User {user.UserName} removed from team {team.Name}");
                    _userTeamRepository.DeleteUserTeam(user.Id, teamId);
                }
                else
                    continue;
            }
            return RedirectToAction("EditTeam", new { Id = teamId });
        }
    }
}
