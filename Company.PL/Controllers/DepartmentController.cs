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


        [HttpGet]
        public IActionResult UpdateForm(int Id)
        {
            Department? department = _departmentRepository.GetById(Id);
            return View(department);
        }


        [HttpPost]
        public IActionResult Update(int Id, Department departmentComing)
        {
            if (ModelState.IsValid)
            {
                Department? department = _departmentRepository.GetById(Id);
                department.Code = departmentComing.Code;
                department.Name = departmentComing.Name;
                department.CreatedAt = departmentComing.CreatedAt;
                int result = _departmentRepository.Update(department);
                if(result > 0)
                    return RedirectToAction("Index");
            }
            return View("UpdateForm", departmentComing);
        }


        public IActionResult Delete(int Id)
        {
            Department? department = _departmentRepository.GetById(Id);
            _departmentRepository.Delete(department);
            return RedirectToAction("Index");
        }


        public IActionResult Details(int Id)
        {
            Department? department = _departmentRepository.GetById(Id);
            return View(department);
        }

    }
}
