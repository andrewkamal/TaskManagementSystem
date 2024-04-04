using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.ViewModels;
using TaskManagementSystem.Models;
using Microsoft.AspNetCore.Hosting;

namespace TaskManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(RegisterUserDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Json(true);
            else
                return Json($"Email {model.Email} is already in use.");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserDTO model)
        {
            if (ModelState.IsValid)
            {
                string FileName = FileProcessing(model);
                var user = new ApplicationUser { 
                    UserName = model.Email, 
                    Email = model.Email, 
                    Department= model.Department, 
                    PhotoPath= FileName
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }
                    _logger.LogWarning("User created a new account successfully.");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        private string FileProcessing(RegisterUserDTO model)
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
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                ViewData["ReturnUrl"] = ReturnUrl;
            else if(string.IsNullOrEmpty(ReturnUrl))
                ViewData["ReturnUrl"] = "Home/Index";
            else
                ViewData["ReturnUrl"] = "Home/Index";
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        _logger.LogWarning("User logged in.");
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        _logger.LogWarning("User logged in.");
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogWarning("User logged out.");
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            _logger.LogWarning("Access Denied.");
            return View();
        }
    }
}
