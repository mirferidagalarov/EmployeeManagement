using Business.Abstract;
using Business.Messages;
using Entities.Concrete.DTOs.CompanyDTOs;
using Entities.Concrete.DTOs.DepartmentDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService; 
        }


        [HttpGet("GetAllDepartment")]
        public IActionResult GetAll()
        {
            var result = _departmentService.GetAllDepartmentCompany();
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result);
        }

        [HttpGet("GetByIdDepartment/{id}")]
        public IActionResult GetId(int id)
        {
            var result = _departmentService.GetDepartmentCompany(id);

            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result);
        }


        [HttpPost("AddDepartment")]
        public IActionResult Add(DepartmentAddDTO  departmentAddDTO)
        {
            var result = _departmentService.Add(departmentAddDTO);

            if (result.Success)
                return Ok(DepartmentMessage.DepartmentAddedSuccesfully);

            return BadRequest(result);
        }


        [HttpPut("EditDepartment")]
        public IActionResult Edit(DepartmentUpdateDTO  departmentUpdateDTO)
        {
            var result = _departmentService.Update(departmentUpdateDTO);
            if (result.Success)
                return Ok(DepartmentMessage.DepartmentUpdateSuccesfully);

            return BadRequest(result);
        }


        [HttpPatch("SoftDeleteDepartment/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _departmentService.Delete(id);
            if (result.Success)
                return Ok(DepartmentMessage.DepartmentDeletedSuccesfully);

            return BadRequest(result);
        }
    }
}
