using AutoMapper;
using Company.DAL.Models;
using Company.PL.Dtos;
using Company.PL.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager,
                              UserManager<AppUser> userManager,
                              IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<IdentityRole> Roles;

            if (string.IsNullOrEmpty(SearchInput))
                Roles = await _roleManager.Roles.ToListAsync();
            else
                Roles = await _roleManager.Roles.Where(Role => Role.Name.ToLower().Contains(SearchInput.ToLower())).ToListAsync();

            IEnumerable<RoleDto> rolesDto = _mapper.Map<IEnumerable<RoleDto>>(Roles);

            return View(rolesDto);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleDto roleDto)
        {
            if (ModelState.IsValid)
            {
                if(! await CheckIfRoleExists(roleDto.Name))
                {
                    IdentityRole Role = new IdentityRole()
                    {
                        Name = roleDto.Name
                    };
                    var Result = await _roleManager.CreateAsync(Role);
                    if (Result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        foreach (var error in Result.Errors)
                            ModelState.AddModelError("", error.Description);
                }
                ModelState.AddModelError("", "Role Is Already Exists !");
            }
            return View(roleDto);
        }



        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (id is null)
                return BadRequest("Id Is Not Valid !");
            else
            {
                IdentityRole? Role = await _roleManager.FindByIdAsync(id);
                if (Role is null)
                    return NotFound(new { StatusCode = 404, Message = $"Role With Id = {id} Is Not Found !" });
                else
                {
                    RoleDto roleDto = _mapper.Map<RoleDto>(Role);
                    return View(roleDto);
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
                IdentityRole? Role = await _roleManager.FindByIdAsync(id);
                if (Role is null)
                    return NotFound(new { StatusCode = 404, Message = $"Role With Id = {id} Is Not Found !" });
                else
                {
                    RoleDto roleDto = _mapper.Map<RoleDto>(Role);
                    return View(roleDto);
                }
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] string id, RoleDto roleDto)
        {
            if (ModelState.IsValid)
            {
                if (id != roleDto.Id)
                    return BadRequest();

                IdentityRole? Role = await _roleManager.FindByIdAsync(id);

                if (User is null)
                    return BadRequest();

                if(!await CheckIfRoleExists(roleDto.Name))
                {
                    _mapper.Map(roleDto, Role);
                    var Result = await _roleManager.UpdateAsync(Role);
                    if (Result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        foreach (var error in Result.Errors)
                            ModelState.AddModelError("", error.Description);
                }
                ModelState.AddModelError("", "Role Is Already Exists !");
            }
            return View(roleDto);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null)
                return BadRequest("Id Is Not Valid !");
            else
            {
                IdentityRole? Role = await _roleManager.FindByIdAsync(id);
                if (Role is null)
                    return NotFound(new { StatusCode = 404, Message = $"Role With Id = {id} Is Not Found !" });
                else
                {
                    RoleDto roleDto = _mapper.Map<RoleDto>(Role);
                    return View(roleDto);
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, RoleDto roleDto)
        {
            if (ModelState.IsValid)
            {
                if (id != roleDto.Id)
                    return BadRequest();
                IdentityRole? Role = await _roleManager.FindByIdAsync(id);
                if (Role is null)
                    return BadRequest();

                var Result = await _roleManager.DeleteAsync(Role);
                if (Result.Succeeded)
                    return RedirectToAction("Index");
                else
                    foreach (var error in Result.Errors)
                        ModelState.AddModelError("", error.Description);
            }
            return View(roleDto);
        }



        public async Task<bool> CheckIfRoleExists(string RoleName)
        {
            var Role = await _roleManager.FindByNameAsync(RoleName);
            return Role is not null;
        }



        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUser(string RoleId)
        {
            IdentityRole? Role = await _roleManager.FindByIdAsync(RoleId);
            if(Role is null)
                return NotFound(new { StatusCode = 404, Message = $"Role With Id = {RoleId} Is Not Found !" });

            ViewData["RoleId"] = RoleId;

            IEnumerable<AppUser> Users = await _userManager.Users.ToListAsync();
            List<UserInRoleDto> usersInRole = new List<UserInRoleDto>();
            foreach(var user in Users)
            {
                UserInRoleDto userInRole = new UserInRoleDto()
                {
                    Id = user.Id,
                    Username = user.UserName,
                    IsSelected = await _userManager.IsInRoleAsync(user, Role.Name)
                };
                usersInRole.Add(userInRole);
            }
            return View(usersInRole);
        }



        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string RoleId, List<UserInRoleDto> usersInRole)
        {
            IdentityRole? Role = await _roleManager.FindByIdAsync(RoleId);
            if (Role is null)
                return NotFound(new { StatusCode = 404, Message = $"Role With Id = {RoleId} Is Not Found !" });

            foreach(var userInRole in usersInRole)
            {
                AppUser? AppUser = await _userManager.FindByIdAsync(userInRole.Id);
                if(User is not null)
                {
                    if(userInRole.IsSelected && ! await _userManager.IsInRoleAsync(AppUser, Role.Name))
                    {
                        await _userManager.AddToRoleAsync(AppUser, Role.Name);
                    }
                    else if(!userInRole.IsSelected && await _userManager.IsInRoleAsync(AppUser, Role.Name))
                    {
                        await _userManager.RemoveFromRoleAsync(AppUser, Role.Name);
                    }
                }
            }
            return RedirectToAction("Update", new { id = RoleId });
        }


    }
}
