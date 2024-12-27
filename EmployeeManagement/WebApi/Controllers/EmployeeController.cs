using Business.Abstract;
using Business.Messages;
using Entities.Concrete.DTOs.DepartmentDTOs;
using Entities.Concrete.DTOs.EmployeeDTOs;
using Entities.Concrete.Pagination;
using Entities.Concrete.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService; 
        }


        [HttpGet("GetAllEmployee")]
        public IActionResult GetAll()
        {
            var result = _employeeService.GetAllEmployee();
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result);
        }

        [HttpGet("GetByIdEmployee/{id}")]
        public IActionResult GetId(int id)
        {
            var result = _employeeService.GetEmployee(id);

            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result);
        }

        [HttpGet("GetAllEmployeeSearch/{text}/{page}/{pageSize}")]
        public IActionResult GetSearch(string text, int page = 1, int pageSize = 10)
        {
            try
            {
                var result = _employeeService.GetEmployeeSearch(text, page, pageSize);
                var resultTotal = _employeeService.GetEmployeeCount(text);

                var viewModel = new EmployeeGetAllPageViewModel
                {
                    PageInfo = new PageInfo()
                    {
                        TotalItems = resultTotal.Data,
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                    },
                    Employees = result.Data
                };

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }


        [HttpPost("AddEmployee")]
        public IActionResult Add(EmployeeAddDTO employeeAddDTO)
        {
            var result = _employeeService.Add(employeeAddDTO);

            if (result.Success)
                return Ok(EmployeeMessage.EmployeeAddedSuccesfully);

            return BadRequest(result);
        }


        [HttpPut("EditEmployee")]
        public IActionResult Edit(EmployeeUpdateDTO  employeeUpdateDTO)
        {
            var result = _employeeService.Update(employeeUpdateDTO);
            if (result.Success)
                return Ok(EmployeeMessage.EmployeeUpdateSuccesfully);

            return BadRequest(result);
        }


        [HttpPatch("SoftDeleteEmployee/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _employeeService.Delete(id);
            if (result.Success)
                return Ok(EmployeeMessage.EmployeeDeletedSuccesfully);

            return BadRequest(result);
        }
    }
}
