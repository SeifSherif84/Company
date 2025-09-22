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
        [ValidateAntiForgeryToken]
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
        public IActionResult Update(int? Id)
        {
            //if (Id is null)
            //    return BadRequest("Invalid Id");
            //Department? department = _departmentRepository.GetById(Id.Value); 
            //if (department == null)
            //    return NotFound(new { StatusCode = 404, Message = $"Department With Id = {Id.Value} Is Not Found" });
            //return View(department); 
            return Details(Id,"Update");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute]int Id ,Department department)
        {
            if (ModelState.IsValid)
            {
                if (Id == department.Id)
                {
                    int result = _departmentRepository.Update(department);
                    if (result > 0)
                        return RedirectToAction("Index");
                }
                else 
                    return BadRequest();
            }
            return View("UpdateForm", department);
        }



        public IActionResult Details(int? Id, string ViewName = "Details")
        {
            if (Id is null)
                return BadRequest("Invalid Id");
            Department? department = _departmentRepository.GetById(Id.Value);
            if (department == null)
                return NotFound(new { StatusCode = 404, Message = $"Department With Id = {Id.Value} Is Not Found" });
            return View(ViewName,department);
        }



        public IActionResult Delete(int? Id)
        {
            //if (Id is null)
            //    return BadRequest("Invalid Id");
            //Department? department = _departmentRepository.GetById(Id.Value);
            //if (department == null)
            //    return NotFound(new { StatusCode = 404, Message = $"Department With Id = {Id.Value} Is Not Found" });
            //return View(department);
            return Details(Id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int Id, Department department)
        {
            if (Id == department.Id)
            {
                int result = _departmentRepository.Delete(department);
                if (result > 0)
                    return RedirectToAction("Index");
            }
            else
                return BadRequest();

            return View(department);
        }



    }
}
