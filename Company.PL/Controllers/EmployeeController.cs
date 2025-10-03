using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Company.PL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Company.PL.DocumentProccessing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Company.PL.Controllers
{

    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(//IEmployeeRepository employeeRepository,
                                  //IDepartmentRepository departmentRepository,
                                  IUnitOfWork unitOfWork,
                                  IMapper mapper)
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> Employees;
            if (string.IsNullOrEmpty(SearchInput))
                Employees = await _unitOfWork._employeeRepository.GetAllAsync();
            else 
                Employees = await _unitOfWork._employeeRepository.GetByNameAsync(SearchInput);
                
            return View(Employees);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork._departmentRepository.GetAllAsync();
            ViewData["Departments"] = departments;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                if(employeeDto.Image is not null)
                    employeeDto.ImageName = DocumentHelper.Upload(employeeDto.Image, "images");

                var employee = _mapper.Map<Employee>(employeeDto);
                await _unitOfWork._employeeRepository.AddAsync(employee);
                int result = await _unitOfWork.SaveAsync();
                if (result > 0)
                    return RedirectToAction("Index");
            }
            return View(employeeDto);
        }



        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest("Id Is Not Valid !");
            else
            {
                Employee? employee = await _unitOfWork._employeeRepository.GetByIdAsync(id.Value);
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
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return BadRequest("Id Is Not Valid !");
            else
            {
                Employee? employee = await _unitOfWork._employeeRepository.GetByIdAsync(id.Value);
                if (employee == null)
                    return NotFound(new { StatusCode = 404, Message = $"Employee With Id = {id.Value} Is Not Found" });
                else
                {
                    var departments = await _unitOfWork._departmentRepository.GetAllAsync();
                    ViewData["Departments"] = departments;
                    var employeeDto = _mapper.Map<EmployeeDto>(employee);
                    return View(employeeDto);
                }
            }
        }

        #region Notes
        //// During HttpGet Update I Get Employee From DB 
        //// Then Make Mapped To Dto To Display In Update Form 
        //// If This Employee Have Image In DB The Value Of ImageName Is Not Null
        //// Employee Not Have Property Called IFormFile Image
        //// Dto Has This(To Recieve It In Client Side In Create Form)
        //// During Mapping Image Property In Dto Is Null
        //// Although This Employee Has Image In DB 
        //// But ImageName Property In Dto Not Null 
        //// Then By ImageName Property Can Check If This Employee Was Has Image Or Not
        //// To Decide Delete Image Or Not But Not Decide By ImageName Only 
        //// Must Sure That He Change His Image Or Not By Image Property
        //// If Become Not Null Then He Change It
        //// Then To Make Delete To Image Then Make Sure 
        //// If This Employee Has Image In DB [By ImageName Property]
        //// And If He Change The Photo [By Image Property Become Has Value]


        //// employeeDto.ImageName is not null --> To Check If This Employee Has Image Or Not (B. If Has Make Delete To His Image If Not The Not Make Delete)
        //// employeeDto.Image is not null --> To Check If He Upload Another Image Or Not

        //// If I Put [employeeDto.ImageName is not null] Only
        //// This Mean During Update
        //// I Check If Employee Has Image In DB Actually Or Not
        //// If I Update Any Another Thing In Form Update And This Employee Has Image
        //// Here Mean Make Delete (برضو)
        //// B. I Check Only If Have Image Make Delete But Must Not Make This
        //// Must Check If Have Image And Change To New Image [employeeDto.Image is not null]
        //// Then Make Delete 
        //// If employeeDto.ImageName is null And employeeDto.Image is not null
        //// Mean This Employee Not Have Image And Upload Image Then Not Make Delete , Upload Only
        //// If employeeDto.ImageName is null And employeeDto.Image is null 
        //// Mean Not Have Image And He Not Upload Image Then Not Make Any Thing
        #endregion


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int id, EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            { 
                if (employeeDto.ImageName is not null && employeeDto.Image is not null) 
                    DocumentHelper.Delete(employeeDto.ImageName, "images");

                if (employeeDto.Image is not null)
                    employeeDto.ImageName = DocumentHelper.Upload(employeeDto.Image, "images");

                var employee = _mapper.Map<Employee>(employeeDto);
                employee.Id = id;
                _unitOfWork._employeeRepository.Update(employee);
                int result = await _unitOfWork.SaveAsync();
                if (result > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(employeeDto);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest("Id Is Not Valid !");
            else
            {
                Employee? employee = await _unitOfWork._employeeRepository.GetByIdAsync(id.Value);
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
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(employeeDto);
                employee.Id = id;
                _unitOfWork._employeeRepository.Delete(employee);
                int result = await _unitOfWork.SaveAsync();
                if (result > 0)
                {
                    if (employeeDto.ImageName is not null)
                        DocumentHelper.Delete(employeeDto.ImageName, "images");
                    return RedirectToAction(nameof(Index));
                }
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
