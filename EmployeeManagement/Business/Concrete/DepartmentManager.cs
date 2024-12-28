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
        private readonly IUnitOfWork _unitOfWork;   
        private readonly IMapper _mapper;
        public DepartmentManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
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

            _unitOfWork.DepartmentDAL.Add(department);
            _unitOfWork.DepartmentDAL.SaveChanges();
            return new SuccessResult(DefaultConstantValues.DATA_ADDED_SUCCESSFULLY);
        }

        public IResult Delete(int id)
        {
            var deleteEntity = _unitOfWork.DepartmentDAL.GetById(x => x.ID == id);

            deleteEntity.Deleted = id;
            _unitOfWork.DepartmentDAL.Update(deleteEntity);
            _unitOfWork.DepartmentDAL.SaveChanges();
            return new SuccessResult(DefaultConstantValues.DATA_DELETED_SUCCESSFULLY);
        }


        public IDataResult<List<DepartmentListDTO>> GetAllDepartmentCompany()
        {
            return new SuccessDataResult<List<DepartmentListDTO>>(_unitOfWork.DepartmentDAL.GetAllDepartmentCompany());
        }

        public IDataResult<DepartmentListDTO> GetDepartmentCompany(int id)
        {
            return new SuccessDataResult<DepartmentListDTO>(_unitOfWork.DepartmentDAL.GetDepartmentCompany(id));
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

            _unitOfWork.DepartmentDAL.Update(department);
            _unitOfWork.DepartmentDAL.SaveChanges();
            return new SuccessResult(DefaultConstantValues.DATA_ADDED_SUCCESSFULLY);
        }

        private IResult CheckDuplicateRow(Department model)
        {
            var allData = _unitOfWork.DepartmentDAL.GetAll(x => x.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE);
            var existingRow = allData.FirstOrDefault(x => x.Name == model.Name);
            if (existingRow != null)
                return new ErrorResult(DefaultConstantValues.DUPLICATE_RECORD_FOUND);

            return new SuccessResult();
        }
    }
}
