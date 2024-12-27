using Core.Results.Abstract;
using Entities.Concrete.DTOs.EmployeeDTOs;

namespace Business.Abstract
{
    public interface IEmployeeService
    {
        IResult Add(EmployeeAddDTO  employeeAddDTO);
        IResult Update(EmployeeUpdateDTO  employeeUpdateDTO);
        IResult Delete(int id);
        IDataResult<List<EmployeeListDTO>> GetAllEmployee();
        IDataResult<EmployeeListDTO> GetEmployee(int id);
        IDataResult<List<EmployeeListDTO>> GetEmployeeSearch(string text, int page, int pageSize);
        IDataResult<int> GetEmployeeCount(string text);
    }
}
