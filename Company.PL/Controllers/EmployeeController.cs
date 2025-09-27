using AutoMapper;
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
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository,
                                  IDepartmentRepository departmentRepository,
                                  IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> Employees;
            if (string.IsNullOrEmpty(SearchInput))
                Employees = _employeeRepository.GetAll();
            else 
                Employees = _employeeRepository.GetByName(SearchInput);
                
            return View(Employees);
        }


        [HttpGet]
        public IActionResult Create()
        {
            //var departments = _departmentRepository.GetAll();
            //ViewData["Departments"] = departments;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(employeeDto);
                int result = _employeeRepository.Add(employee);
                if (result > 0)
                    return RedirectToAction("Index");
            }
            return View(employeeDto);
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
                    var employeeDto = _mapper.Map<EmployeeDto>(employee);
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
                    //var departments = _departmentRepository.GetAll();
                    //ViewData["Departments"] = departments;
                    var employeeDto = _mapper.Map<EmployeeDto>(employee);
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
                var employee = _mapper.Map<Employee>(employeeDto);
                employee.Id = id;
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
                    var employeeDto = _mapper.Map<EmployeeDto>(employee);
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
                //var employee = _mapper.Map<Employee>(employeeDto);
                var employee = _employeeRepository.GetById(id);
                //employee.Id = id;
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
