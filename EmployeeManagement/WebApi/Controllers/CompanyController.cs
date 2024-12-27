using Business.Abstract;
using Business.Messages;
using Entities.Concrete.DTOs.CompanyDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService=companyService;
        }

        [HttpGet("GetAllCompany")]
        public IActionResult GetAll()
        {
            var result = _companyService.GetAll();
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result);
        }

        [HttpGet("GetByIdCompany/{id}")]
        public IActionResult GetId(int id)
        {
            var result = _companyService.Get(id);

            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result);
        }

        [HttpPost("AddCompany")]
        public IActionResult Add(CompanyAddDTO  companyAddDTO)
        {
            var result = _companyService.Add(companyAddDTO);

            if (result.Success)
                return Ok(CompanyMessage.CompanyAddedSuccesfully);

            return BadRequest(result);
        }

        [HttpPut("EditCompany")]
        public IActionResult Edit(CompanyUpdateDTO  companyUpdateDTO)
        {
            var result = _companyService.Update(companyUpdateDTO);
            if (result.Success)
                return Ok(CompanyMessage.CompanyUpdateSuccesfully);

            return BadRequest(result);
        }


        [HttpPatch("SoftDeleteCompany/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _companyService.Delete(id);
            if (result.Success)
                return Ok(CompanyMessage.CompanyDeletedSuccesfully);

            return BadRequest(result);
        }

    }
}
