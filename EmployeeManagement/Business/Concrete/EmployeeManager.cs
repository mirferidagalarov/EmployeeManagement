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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork; 
            _mapper = mapper;   
        }

        public IResult Add(EmployeeAddDTO employeeAddDTO)
        {
            Employee employee = _mapper.Map<Employee>(employeeAddDTO);

            var validationResult = ValidationTool.Validate(new EmployeeValidation(), employee, out List<ValidationErrorModel> errors);
            if (!validationResult)
                return new ErrorResult(errors.ValidationErrorMessagesWithNewLine());

            _unitOfWork.EmployeeDAL.Add(employee);
            _unitOfWork.EmployeeDAL.SaveChanges();
            return new SuccessResult(DefaultConstantValues.DATA_ADDED_SUCCESSFULLY);
        }

        public IResult Delete(int id)
        {
            var deleteEntity = _unitOfWork.EmployeeDAL.GetById(x => x.ID == id);

            deleteEntity.Deleted = id;
            _unitOfWork.EmployeeDAL.Update(deleteEntity);
            _unitOfWork.EmployeeDAL.SaveChanges();
            return new SuccessResult(DefaultConstantValues.DATA_DELETED_SUCCESSFULLY);
        }

        public IDataResult<List<EmployeeListDTO>> GetAllEmployee()
        {
            return new SuccessDataResult<List<EmployeeListDTO>>(_unitOfWork.EmployeeDAL.GetAllEmployee());
        }

        public IDataResult<EmployeeListDTO> GetEmployee(int id)
        {
            return new SuccessDataResult<EmployeeListDTO>(_unitOfWork.EmployeeDAL.GetEmployee(id));
        }

        public IDataResult<int> GetEmployeeCount(string text)
        {
            return new SuccessDataResult<int>(_unitOfWork.EmployeeDAL.EmployeeSearchCount(text));
        }

        public IDataResult<List<EmployeeListDTO>> GetEmployeeSearch(string text, int page, int pageSize)
        {
            if (page <= 1)
            {
                page = 0;
            }

            return new SuccessDataResult<List<EmployeeListDTO>>(_unitOfWork.EmployeeDAL.EmployeeSearch(text).ToList());
        }

        public IResult Update(EmployeeUpdateDTO employeeUpdateDTO)
        {
            Employee employee = _mapper.Map<Employee>(employeeUpdateDTO);

            var validationResult = ValidationTool.Validate(new EmployeeValidation(), employee, out List<ValidationErrorModel> errors);
            if (!validationResult)
                return new ErrorResult(errors.ValidationErrorMessagesWithNewLine());

            _unitOfWork.EmployeeDAL.Update(employee);
            _unitOfWork.EmployeeDAL.SaveChanges();
            return new SuccessResult(DefaultConstantValues.DATA_ADDED_SUCCESSFULLY);
        }
    }
}
