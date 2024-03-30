﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.ViewModels;

namespace TaskManagementSystem.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AdministrationController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        public AdministrationController(RoleManager<IdentityRole> roleManager, ILogger<AdministrationController> logger, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;

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
                    _logger.LogInformation($"Role {model.RoleName} created successfully");
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
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
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
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                else
                    continue;
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }
    }
}
