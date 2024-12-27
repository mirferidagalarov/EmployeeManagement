using AutoMapper;
using Business.Abstract;
using Business.Validations;
using Core.Constants;
using Core.Extensions;
using Core.Results.Abstract;
using Core.Results.Concrete;
using Core.Validation;
using DataAccess.Abstract;
using Entities.Concrete.DTOs.EmployeeDTOs;
using Entities.Concrete.TableModels;

namespace Business.Concrete
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IEmployeeDAL _employeeDAL;
        private readonly IMapper _mapper;
        public EmployeeManager(IEmployeeDAL employeeDAL,IMapper mapper)
        {
            _employeeDAL = employeeDAL; 
            _mapper = mapper;   
        }

        public IResult Add(EmployeeAddDTO employeeAddDTO)
        {
            Employee employee = _mapper.Map<Employee>(employeeAddDTO);

            var validationResult = ValidationTool.Validate(new EmployeeValidation(), employee, out List<ValidationErrorModel> errors);
            if (!validationResult)
                return new ErrorResult(errors.ValidationErrorMessagesWithNewLine());

            _employeeDAL.Add(employee);
            _employeeDAL.SaveChanges();
            return new SuccessResult(DefaultConstantValues.DATA_ADDED_SUCCESSFULLY);
        }

        public IResult Delete(int id)
        {
            var deleteEntity = _employeeDAL.GetById(x => x.ID == id);

            deleteEntity.Deleted = id;
            _employeeDAL.Update(deleteEntity);
            _employeeDAL.SaveChanges();
            return new SuccessResult(DefaultConstantValues.DATA_DELETED_SUCCESSFULLY);
        }

        public IDataResult<List<EmployeeListDTO>> GetAllEmployee()
        {
            return new SuccessDataResult<List<EmployeeListDTO>>(_employeeDAL.GetAllEmployee());
        }

        public IDataResult<EmployeeListDTO> GetEmployee(int id)
        {
            return new SuccessDataResult<EmployeeListDTO>(_employeeDAL.GetEmployee(id));
        }

        public IDataResult<int> GetEmployeeCount(string text)
        {
            return new SuccessDataResult<int>(_employeeDAL.EmployeeSearchCount(text));
        }

        public IDataResult<List<EmployeeListDTO>> GetEmployeeSearch(string text, int page, int pageSize)
        {
            if (page <= 1)
            {
                page = 0;
            }

            return new SuccessDataResult<List<EmployeeListDTO>>(_employeeDAL.EmployeeSearch(text).ToList());
        }

        public IResult Update(EmployeeUpdateDTO employeeUpdateDTO)
        {
            Employee employee = _mapper.Map<Employee>(employeeUpdateDTO);

            var validationResult = ValidationTool.Validate(new EmployeeValidation(), employee, out List<ValidationErrorModel> errors);
            if (!validationResult)
                return new ErrorResult(errors.ValidationErrorMessagesWithNewLine());

            _employeeDAL.Update(employee);
            _employeeDAL.SaveChanges();
            return new SuccessResult(DefaultConstantValues.DATA_ADDED_SUCCESSFULLY);
        }
    }
}
