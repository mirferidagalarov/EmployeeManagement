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
using Entities.Concrete.DTOs.CompanyDTOs;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Authorization;

namespace Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CompanyManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IResult Add(CompanyAddDTO companyAddDTO)
        {
            Company company = _mapper.Map<Company>(companyAddDTO);

            var validationResult = ValidationTool.Validate(new CompanyValidation(), company, out List<ValidationErrorModel> errors);
            if (!validationResult)
                return new ErrorResult(errors.ValidationErrorMessagesWithNewLine());

            var failedBusinessLogic = BusinessRules.Execute(CheckDuplicateRow(company));
            if (failedBusinessLogic is not null)
                return failedBusinessLogic;

            _unitOfWork.CompanyDAL.Add(company);
            _unitOfWork.SaveAsync();
            return new SuccessResult(DefaultConstantValues.DATA_ADDED_SUCCESSFULLY);
        }

        public IResult Delete(int id)
        {
            var deleteEntity = _unitOfWork.CompanyDAL.GetById(x => x.ID == id);

            deleteEntity.Deleted = id;
            _unitOfWork.CompanyDAL.Update(deleteEntity);
            _unitOfWork.CompanyDAL.SaveChanges();
            return new SuccessResult(DefaultConstantValues.DATA_DELETED_SUCCESSFULLY);
        }

        public IDataResult<Company> Get(int id)
        {
            return new SuccessDataResult<Company>(_unitOfWork.CompanyDAL.GetById(x => x.ID == id && x.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE));
        }

        public IDataResult<List<CompanyListDTO>> GetAll()
        {
            List<Company> companies = _unitOfWork.CompanyDAL.GetAll(x => x.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE);
            return new SuccessDataResult<List<CompanyListDTO>>(_mapper.Map<List<CompanyListDTO>>(companies));
        }

        public IResult Update(CompanyUpdateDTO companyUpdateDTO)
        {
            Company company = _mapper.Map<Company>(companyUpdateDTO);

            var validationResult = ValidationTool.Validate(new CompanyValidation(), company, out List<ValidationErrorModel> errors);
            if (!validationResult)
                return new ErrorResult(errors.ValidationErrorMessagesWithNewLine());

            var failedBusinessLogic = BusinessRules.Execute(CheckDuplicateRow(company));
            if (failedBusinessLogic is not null)
                return failedBusinessLogic;


            _unitOfWork.CompanyDAL.Update(company);
            _unitOfWork.CompanyDAL.SaveChanges();
            return new SuccessResult(DefaultConstantValues.DATA_ADDED_SUCCESSFULLY);
        }


        private IResult CheckDuplicateRow(Company model)
        {
            var allData = _unitOfWork.CompanyDAL.GetAll(x => x.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE);
            var existingRow = allData.FirstOrDefault(x => x.Name == model.Name);
            if (existingRow != null)
                return new ErrorResult(DefaultConstantValues.DUPLICATE_RECORD_FOUND);

            return new SuccessResult();
        }
    }
}
