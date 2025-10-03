using Company.DAL.Models;
using Company.PL.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, 
                                 SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegistrationDto model)
        {
            if (ModelState.IsValid)
            {
                AppUser? User = await _userManager.FindByNameAsync(model.UserName);
                if (User is null)
                {
                    User = await _userManager.FindByEmailAsync(model.Email);
                    if (User is null)
                    {
                        // Register
                        User = new AppUser()
                        {
                            UserName = model.UserName,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            IsAgree = model.IsAgree,
                        };
                        var result = await _userManager.CreateAsync(User, model.Password);
                        if (result.Succeeded)
                            return RedirectToAction(nameof(Login));
                        else
                            foreach (var error in result.Errors)
                                ModelState.AddModelError("", error.Description);
                    }
                }
                ModelState.AddModelError("", "Invalid Registration !");
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (ModelState.IsValid)
            {
                AppUser? User = await _userManager.FindByEmailAsync(model.Email);
                if(User is not null)
                {
                    bool Flag = await _userManager.CheckPasswordAsync(User, model.Password);
                    if (Flag)
                    {
                        // Login
                        var result = await _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
                        if (result.Succeeded)                     
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                }
                ModelState.AddModelError("", "Invalid Login !");
            }
            return View(model);
        }


    }
}
