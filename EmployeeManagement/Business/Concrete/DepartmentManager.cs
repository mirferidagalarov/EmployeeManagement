using AutoMapper;
using Business.Abstract;
using Business.Validations;
using Core.Business;
using Core.Constants;
using Core.Extensions;
using Core.Results.Abstract;
using Core.Results.Concrete;
using Core.Validation;
using DataAccess.Abstract;
using Entities.Concrete.DTOs.DepartmentDTOs;
using Entities.Concrete.TableModels;

namespace Business.Concrete
{
    public class DepartmentManager : IDepartmentService
    {
        private readonly IDepartmentDAL _departmentDAL;
        private readonly IMapper _mapper;
        public DepartmentManager(IDepartmentDAL departmentDAL,IMapper mapper)
        {
            _departmentDAL = departmentDAL;
            _mapper = mapper;   
        }
        public IResult Add(DepartmentAddDTO departmentAddDTO)
        {
            Department department = _mapper.Map<Department>(departmentAddDTO);

            var validationResult = ValidationTool.Validate(new DepartmentValidation(), department, out List<ValidationErrorModel> errors);
            if (!validationResult)
                return new ErrorResult(errors.ValidationErrorMessagesWithNewLine());

            var failedBusinessLogic = BusinessRules.Execute(CheckDuplicateRow(department));
            if (failedBusinessLogic is not null)
                return failedBusinessLogic;

            _departmentDAL.Add(department);
            _departmentDAL.SaveChanges();
            return new SuccessResult(DefaultConstantValues.DATA_ADDED_SUCCESSFULLY);
        }

        public IResult Delete(int id)
        {
            var deleteEntity = _departmentDAL.GetById(x => x.ID == id);

            deleteEntity.Deleted = id;
            _departmentDAL.Update(deleteEntity);
            _departmentDAL.SaveChanges();
            return new SuccessResult(DefaultConstantValues.DATA_DELETED_SUCCESSFULLY);
        }


        public IDataResult<List<DepartmentListDTO>> GetAllDepartmentCompany()
        {
            return new SuccessDataResult<List<DepartmentListDTO>>(_departmentDAL.GetAllDepartmentCompany());
        }

        public IDataResult<DepartmentListDTO> GetDepartmentCompany(int id)
        {
            return new SuccessDataResult<DepartmentListDTO>(_departmentDAL.GetDepartmentCompany(id));
        }

        public IResult Update(DepartmentUpdateDTO departmentUpdateDTO)
        {
            Department department = _mapper.Map<Department>(departmentUpdateDTO);

            var validationResult = ValidationTool.Validate(new DepartmentValidation(), department, out List<ValidationErrorModel> errors);
            if (!validationResult)
                return new ErrorResult(errors.ValidationErrorMessagesWithNewLine());

            var failedBusinessLogic = BusinessRules.Execute(CheckDuplicateRow(department));
            if (failedBusinessLogic is not null)
                return failedBusinessLogic;

            _departmentDAL.Update(department);
            _departmentDAL.SaveChanges();
            return new SuccessResult(DefaultConstantValues.DATA_ADDED_SUCCESSFULLY);
        }

        private IResult CheckDuplicateRow(Department model)
        {
            var allData = _departmentDAL.GetAll(x => x.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE);
            var existingRow = allData.FirstOrDefault(x => x.Name == model.Name);
            if (existingRow != null)
                return new ErrorResult(DefaultConstantValues.DUPLICATE_RECORD_FOUND);

            return new SuccessResult();
        }
    }
}
