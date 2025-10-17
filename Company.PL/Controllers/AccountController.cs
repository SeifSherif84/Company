using Company.DAL.Models;
using Company.PL.Dtos;
using Company.PL.Helper.MailKitFeature;
using Company.PL.Helper.TwilioFeature;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Facebook;



namespace Company.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMailService _mailService;
        private readonly ITwilioService _twilioService;

        public AccountController(UserManager<AppUser> userManager, 
                                 SignInManager<AppUser> signInManager,
                                 IMailService mailService,
                                 ITwilioService twilioService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
            _twilioService = twilioService;
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
                            PhoneNumber = model.PhoneNumber,
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


        [HttpGet]
        public IActionResult ForgetPasswordUsingEmail()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ForgetPasswordUsingPhone()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> SendResetPasswordEmailUrl(ForgetPasswordByEmailDto model)
        {
            if(ModelState.IsValid)
            {
                AppUser? User = await _userManager.FindByEmailAsync(model.Email);
                if(User is not null)
                {

                    var Token = await _userManager.GeneratePasswordResetTokenAsync(User);

                    var URL = Url.Action("ResetPassword",
                                      "Account",
                                                new { model.Email, Token },
                                                Request.Scheme);

                    Email email = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body = URL
                    };

                    bool Flag = _mailService.SendMail(email);
                    if (Flag)
                        return RedirectToAction("CheckYourInbox");
                    else
                        return BadRequest();
                }
                ModelState.AddModelError("", "Invalid Email !");
            }
            return View("ForgetPasswordUsingEmail", model);
        }



        [HttpPost]
        public async Task<IActionResult> SendResetPasswordSmsUrl(ForgetPasswordByPhoneDto model)
        {
            if (ModelState.IsValid)
            {
                model.PhoneNumber =  model.PhoneNumber;
                AppUser? User = _userManager.Users.FirstOrDefault(User => User.PhoneNumber == model.PhoneNumber);
                if (User is not null)
                {

                    var Token = await _userManager.GeneratePasswordResetTokenAsync(User);

                    var URL = Url.Action("ResetPassword",
                                      "Account",
                                                new { model.PhoneNumber, Token },
                                                Request.Scheme);

                    SMS sms = new SMS()
                    {
                        To = model.PhoneNumber,
                        Body = URL
                    };


                    if (!sms.To.StartsWith("+20"))
                    {
                        sms.To = "+20" + sms.To.TrimStart('0');
                    }


                    var Result = _twilioService.SendSms(sms);
                    if (Result.ErrorCode is null)
                        return RedirectToAction("CheckYourSmsMessage");
                    else
                        return BadRequest();
                }
                ModelState.AddModelError("", "Invalid Phone !");
            }
            return View("ForgetPasswordUsingPhone", model);
        }


        public IActionResult CheckYourSmsMessage()
        {
            return View();
        }



        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ResetPassword(string? Email, string Token, string? Phone)
        {
            TempData["Email"] = Email;
            TempData["Token"] = Token;
            TempData["Phone"] = Phone;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (ModelState.IsValid)
            {

                var Email = TempData["Email"] as string;
                var Token = TempData["Token"] as string;
                var Phone = TempData["Phone"] as string;

                if(Phone is null)
                {
                    if (Email != null && Token != null)
                    {
                        AppUser? User = await _userManager.FindByEmailAsync(Email);
                        if (User != null)
                        {
                            var Result = await _userManager.ResetPasswordAsync(User, Token, model.NewPassword);
                            if (Result.Succeeded)
                                return RedirectToAction("Login");
                            else
                                foreach (var error in Result.Errors)
                                    ModelState.AddModelError("", error.Description);
                        }
                        ModelState.AddModelError("", "Account Not Found !");
                    }
                    return BadRequest("Invalid Request !");
                }

                if(Email is null)
                {
                    if (Phone != null && Token != null)
                    {
                        AppUser? User = await _userManager.FindByEmailAsync(Phone);
                        if (User != null)
                        {
                            var Result = await _userManager.ResetPasswordAsync(User, Token, model.NewPassword);
                            if (Result.Succeeded)
                                return RedirectToAction("Login");
                            else
                                foreach (var error in Result.Errors)
                                    ModelState.AddModelError("", error.Description);
                        }
                        ModelState.AddModelError("", "Account Not Found !");
                    }
                    return BadRequest("Invalid Request !");
                }

            }
            return View(model);
        }


        public IActionResult AccessDenied()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }



        public IActionResult GoogleLogin()
        {
            var prop = new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            };
            return Challenge(prop, GoogleDefaults.AuthenticationScheme);
        }


        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault()?.Claims.Select(
                claim => new
                {
                    claim.Type,
                    claim.Value,
                    claim.Issuer,
                    claim.OriginalIssuer
                }
            );
            return RedirectToAction("Index", "Home");
        }



        public IActionResult FacebookLogin()
        {
            var prop = new AuthenticationProperties()
            {
                RedirectUri = Url.Action("FacebookResponse")
            };
            return Challenge(prop, FacebookDefaults.AuthenticationScheme);
        }



        public async Task<IActionResult> FacebookResponse()
        {
            var result = await HttpContext.AuthenticateAsync(FacebookDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault()?.Claims.Select(
                claim => new
                {
                    claim.Type,
                    claim.Value,
                    claim.Issuer,
                    claim.OriginalIssuer
                }
            );
            return RedirectToAction("Index", "Home");
        }



    }
}
