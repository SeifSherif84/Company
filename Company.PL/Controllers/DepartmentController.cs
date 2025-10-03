using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Company.PL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    [Authorize] 
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork._departmentRepository.GetAllAsync();
            return View(departments);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentDto departmentDto)
        {
            if (ModelState.IsValid) 
            {
                var department = _mapper.Map<Department>(departmentDto);
                await _unitOfWork._departmentRepository.AddAsync(department);
                var result = await _unitOfWork.SaveAsync();
                if (result > 0)
                    return RedirectToAction("Index");
            }
            return View(departmentDto);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest("Id Is Not Valid !");
            else
            {
                Department? department = await _unitOfWork._departmentRepository.GetByIdAsync(id.Value);
                if (department == null)
                    return NotFound(new { StatusCode = 404, Message = $"Department With Id = {id.Value} Is Not Found" });
                else
                {
                    var departmentDto = _mapper.Map<DepartmentDto>(department);
                    return View(departmentDto);
                }
            }
        }



        [HttpGet]
        public  async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return BadRequest("Id Is Not Valid !");
            else
            {
                Department? department = await _unitOfWork._departmentRepository.GetByIdAsync(id.Value);
                if (department == null)
                    return NotFound(new { StatusCode = 404, Message = $"Department With Id = {id.Value} Is Not Found" });
                else
                {
                    var departmentDto = _mapper.Map<DepartmentDto>(department);
                    return View(departmentDto);
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int id, DepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                var department = _mapper.Map<Department>(departmentDto);
                department.Id = id;
                _unitOfWork._departmentRepository.Update(department);
                int result = await _unitOfWork.SaveAsync();
                if (result > 0)
                    return RedirectToAction("Index");
            }
            return View(departmentDto);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest("Id Is Not Valid !");
            else
            {
                Department? department = await _unitOfWork._departmentRepository.GetByIdAsync(id.Value);
                if (department == null)
                    return NotFound(new { StatusCode = 404, Message = $"Department With Id = {id.Value} Is Not Found" });
                else
                {
                    var departmentDto = _mapper.Map<DepartmentDto>(department);
                    return View(departmentDto);
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, DepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                var department = _mapper.Map<Department>(departmentDto);
                department.Id = id;
                _unitOfWork._departmentRepository.Delete(department);
                int result = await _unitOfWork.SaveAsync();
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
