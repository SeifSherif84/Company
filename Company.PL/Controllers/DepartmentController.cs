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
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest("Id Is Not Valid !");
            else
            {
                Department? department = _departmentRepository.GetById(id.Value);
                if (department == null)
                    return NotFound(new { StatusCode = 404, Message = $"Department With Id = {id.Value} Is Not Found" });
                else
                {
                    DepartmentDto departmentDto = new DepartmentDto()
                    {
                        Code = department.Code,
                        Name = department.Name,
                        CreatedAt = department.CreatedAt,
                    };
                    return View(departmentDto);
                }
            }
        }



        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null)
                return BadRequest("Id Is Not Valid !");
            else
            {
                Department? department = _departmentRepository.GetById(id.Value);
                if (department == null)
                    return NotFound(new { StatusCode = 404, Message = $"Department With Id = {id.Value} Is Not Found" });
                else
                {
                    DepartmentDto departmentDto = new DepartmentDto()
                    {
                        Code = department.Code,
                        Name = department.Name,
                        CreatedAt = department.CreatedAt,
                    };
                    return View(departmentDto);
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute] int id, DepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                Department department = new Department()
                {
                    Id = id,
                    Code = departmentDto.Code,
                    Name = departmentDto.Name,
                    CreatedAt = departmentDto.CreatedAt,
                };
                int result = _departmentRepository.Update(department);
                if (result > 0)
                    return RedirectToAction("Index");
            }
            return View(departmentDto);
        }



        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest("Id Is Not Valid !");
            else
            {
                Department? department = _departmentRepository.GetById(id.Value);
                if (department == null)
                    return NotFound(new { StatusCode = 404, Message = $"Department With Id = {id.Value} Is Not Found" });
                else
                {
                    DepartmentDto departmentDto = new DepartmentDto()
                    {
                        Code = department.Code,
                        Name = department.Name,
                        CreatedAt = department.CreatedAt,
                    };
                    return View(departmentDto);
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, DepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                Department department = new Department()
                {
                    Id = id,
                    Code = departmentDto.Code,
                    Name = departmentDto.Name,
                    CreatedAt = departmentDto.CreatedAt,
                };
                int result = _departmentRepository.Delete(department);
                if (result > 0)
                    return RedirectToAction("Index");
            }
            return View(departmentDto);
        }



        #region Action_After_Using_Dto
        //[HttpGet]
        //public IActionResult Details(int? id, string ViewName = "Details")
        //{
        //    if (id == null)
        //        return BadRequest("Id Is Not Valid !");
        //    else
        //    {
        //        Department? department = _departmentRepository.GetById(id.Value);
        //        if (department == null)
        //            return NotFound(new { StatusCode = 404, Message = $"Employee With Id = {id.Value} Is Not Found" });
        //        else
        //            return View(ViewName, department);
        //    }
        //}



        //[HttpGet]
        //public IActionResult Update(int? id)
        //{
        //    //if (id == null)
        //    //    return BadRequest("Id Is Not Valid !");
        //    //else
        //    //{
        //    //    Department? department = _departmentRepository.GetById(id.Value);
        //    //    if (department == null)
        //    //        return NotFound(new { StatusCode = 404, Message = $"Employee With Id = {id.Value} Is Not Found" });
        //    //    else
        //    //        return View(department);
        //    //}
        //    return Details(id,"Update");
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Update([FromRoute]int Id ,Department department)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (Id == department.Id)
        //        {
        //            int result = _departmentRepository.Update(department);
        //            if (result > 0)
        //                return RedirectToAction("Index");
        //        }
        //        else 
        //            return BadRequest();
        //    }
        //    return View(department);
        //}



        //public IActionResult Delete(int? id)
        //{
        //    //if (id == null)
        //    //    return BadRequest("Id Is Not Valid !");
        //    //else
        //    //{
        //    //    Department? department = _departmentRepository.GetById(id.Value);
        //    //    if (department == null)
        //    //        return NotFound(new { StatusCode = 404, Message = $"Employee With Id = {id.Value} Is Not Found" });
        //    //    else
        //    //        return View(department);
        //    //}
        //    return Details(id,"Delete");
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete([FromRoute] int Id, Department department)
        //{
        //    if (Id == department.Id)
        //    {
        //        int result = _departmentRepository.Delete(department);
        //        if (result > 0)
        //            return RedirectToAction("Index");
        //    }
        //    else
        //        return BadRequest();

        //    return View(department);
        //}

        #endregion


    }
}
