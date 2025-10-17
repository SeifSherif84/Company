using AutoMapper;
using Company.DAL.Models;
using Company.PL.Dtos;
using Company.PL.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


#region Get UserLoginId 
// string? UserLoginId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//AppUser? appUser = await _userManager.GetUserAsync(User);
//string? UserLoginId = appUser?.Id; 
#endregion

namespace Company.PL.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<AppUser> userManager,
                              IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<AppUser> Users;

            if (string.IsNullOrEmpty(SearchInput))
                Users = await _userManager.Users.ToListAsync();

            else
                Users = await _userManager.Users.Where(u => u.FirstName.ToLower().Contains(SearchInput.ToLower()))
                                                .ToListAsync();

            var userDtos = new List<UserDto>();

            foreach (var user in Users)
            {
                var dto = _mapper.Map<UserDto>(user);
                dto.Roles = await _userManager.GetRolesAsync(user);
                userDtos.Add(dto);
            }
            return View(userDtos);
        }




        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (id is null)
                return BadRequest("Id Is Not Valid !");
            else
            {
                AppUser? User = await _userManager.FindByIdAsync(id);
                if (User is null)
                    return NotFound(new { StatusCode = 404, Message = $"User With Id = {id} Is Not Found !" });
                else
                {
                    var userDto = _mapper.Map<UserDto>(User);
                    userDto.Roles = await _userManager.GetRolesAsync(User);
                    return View(userDto);
                }
            }
        }



        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            if (id is null)
                return BadRequest("Id Is Not Valid !");
            else
            {
                AppUser? User = await _userManager.FindByIdAsync(id);
                if (User is null)
                    return NotFound(new { StatusCode = 404, Message = $"User With Id = {id} Is Not Found !" });
                else
                {
                    var userDto = _mapper.Map<UserDto>(User);
                    userDto.Roles = await _userManager.GetRolesAsync(User);
                    return View(userDto);
                }
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] string id, UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                if (id != userDto.Id) 
                    return BadRequest();
                AppUser? User = await _userManager.FindByIdAsync(id);
                if (User is null)
                    return BadRequest();

                _mapper.Map(userDto, User);
                var Result = await _userManager.UpdateAsync(User);
                if (Result.Succeeded)
                    return RedirectToAction("Index");
                else
                    foreach (var error in Result.Errors)
                        ModelState.AddModelError("", error.Description);
            }
            return View(userDto);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null)
                return BadRequest("Id Is Not Valid !");
            else
            {
                AppUser? User = await _userManager.FindByIdAsync(id);
                if (User is null)
                    return NotFound(new { StatusCode = 404, Message = $"User With Id = {id} Is Not Found !" });
                else
                {
                    var userDto = _mapper.Map<UserDto>(User);
                    userDto.Roles = await _userManager.GetRolesAsync(User);
                    return View(userDto);
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                if (id != userDto.Id)
                    return BadRequest();
                AppUser? User = await _userManager.FindByIdAsync(id);
                if (User is null)
                    return BadRequest();

                var Result = await _userManager.DeleteAsync(User);
                if (Result.Succeeded)
                    return RedirectToAction("Index");
                else
                    foreach (var error in Result.Errors)
                        ModelState.AddModelError("", error.Description);
            }
            return View(userDto);
        }



        public bool CheckIfUserExists(string? username, string? Email)
        {
            var user = _userManager.Users.FirstOrDefault(User => User.UserName == username || User.Email == Email);
            return user is not null;
        }

    }
}
