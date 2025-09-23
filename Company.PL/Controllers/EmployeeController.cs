using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Company.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var Employees = _employeeRepository.GetAll();
            return View(Employees);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    Name = model.Name,
                    Age = model.Age,
                    Email = model.Email,
                    Address = model.Address,
                    Phone = model.Phone,
                    Salary = model.Salary,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    HiringDate = model.HiringDate,
                    CreateAt = model.CreateAt
                };
                int result = _employeeRepository.Add(employee);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }



        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest("Id Is Not Valid !");
            else
            {
                Employee? employee = _employeeRepository.GetById(id.Value);
                if (employee == null)
                    return NotFound(new { StatusCode = 404, Message = $"Employee With Id = {id.Value} Is Not Found" });
                else
                {
                    EmployeeDto employeeDto = new EmployeeDto()
                    {
                        Name = employee.Name,
                        Address = employee.Address,
                        Age = employee.Age,
                        CreateAt = employee.CreateAt,
                        HiringDate = employee.HiringDate,
                        Email = employee.Email,
                        IsActive = employee.IsActive,
                        IsDeleted = employee.IsDeleted,
                        Phone = employee.Phone,
                        Salary = employee.Salary
                    };
                    return View(employeeDto);
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
                Employee? employee = _employeeRepository.GetById(id.Value);
                if (employee == null)
                    return NotFound(new { StatusCode = 404, Message = $"Employee With Id = {id.Value} Is Not Found" });
                else
                {
                    EmployeeDto employeeDto = new EmployeeDto()
                    {
                        Name = employee.Name,
                        Address = employee.Address,
                        Age = employee.Age,
                        CreateAt = employee.CreateAt,
                        HiringDate = employee.HiringDate,
                        Email = employee.Email,
                        IsActive = employee.IsActive,
                        IsDeleted = employee.IsDeleted,
                        Phone = employee.Phone,
                        Salary = employee.Salary
                    };
                    return View(employeeDto);
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute] int id, EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    Id = id,
                    Name = employeeDto.Name,
                    Address = employeeDto.Address,
                    Age = employeeDto.Age,
                    CreateAt = employeeDto.CreateAt,
                    HiringDate = employeeDto.HiringDate,
                    Email = employeeDto.Email,
                    IsActive = employeeDto.IsActive,
                    IsDeleted = employeeDto.IsDeleted,
                    Phone = employeeDto.Phone,
                    Salary = employeeDto.Salary
                };
                int result = _employeeRepository.Update(employee);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(employeeDto);
        }



        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest("Id Is Not Valid !");
            else
            {
                Employee? employee = _employeeRepository.GetById(id.Value);
                if (employee == null)
                    return NotFound(new { StatusCode = 404, Message = $"Employee With Id = {id.Value} Is Not Found" });
                else
                {
                    EmployeeDto employeeDto = new EmployeeDto()
                    {
                        Name = employee.Name,
                        Address = employee.Address,
                        Age = employee.Age,
                        CreateAt = employee.CreateAt,
                        HiringDate = employee.HiringDate,
                        Email = employee.Email,
                        IsActive = employee.IsActive,
                        IsDeleted = employee.IsDeleted,
                        Phone = employee.Phone,
                        Salary = employee.Salary
                    };
                    return View(employeeDto);
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    Id = id,
                    Name = employeeDto.Name,
                    Address = employeeDto.Address,
                    Age = employeeDto.Age,
                    CreateAt = employeeDto.CreateAt,
                    HiringDate = employeeDto.HiringDate,
                    Email = employeeDto.Email,
                    IsActive = employeeDto.IsActive,
                    IsDeleted = employeeDto.IsDeleted,
                    Phone = employeeDto.Phone,
                    Salary = employeeDto.Salary
                };
                int result = _employeeRepository.Delete(employee);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(employeeDto);
        }



        #region Action_After_Using_Dto
        //[HttpGet]
        //public IActionResult Update(int? id)
        //{
        //    return Details(id, "Update");
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Update([FromRoute] int id, Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (id == employee.Id)
        //        {
        //            int result = _employeeRepository.Update(employee);
        //            if (result > 0)
        //                return RedirectToAction(nameof(Index));
        //        }
        //        else
        //            return BadRequest();
        //    }
        //    return View(employee);
        //}



        //[HttpGet]
        //public IActionResult Details(int? id , string ViewName = "Details")
        //{
        //    if (id == null)
        //        return BadRequest("Id Is Not Valid !");
        //    else
        //    {
        //        Employee? employee = _employeeRepository.GetById(id.Value);
        //        if (employee == null)
        //            return NotFound(new { StatusCode = 404, Message = $"Employee With Id = {id.Value} Is Not Found" });
        //        else
        //            return View(ViewName,employee);
        //    }
        //}


        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    return Details(id, "Delete");
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete([FromRoute] int id, Employee employee)
        //{
        //    if (id == employee.Id)
        //    {
        //        int result = _employeeRepository.Delete(employee);
        //        if (result > 0)
        //            return RedirectToAction(nameof(Index));
        //    }
        //    else
        //        return BadRequest();
        //    return View(employee);
        //}

        #endregion


    }
}
