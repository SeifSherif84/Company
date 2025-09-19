using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Company.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentDto departmentDto)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                Department department = new Department()
                {
                    Code = departmentDto.Code,
                    Name = departmentDto.Name,
                    CreatedAt = departmentDto.CreatedAt,
                };
                var result = _departmentRepository.Add(department);
                if(result > 0)
                    return RedirectToAction("Index");
            }
            return View(departmentDto);
        }
    }
}
