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

namespace Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        private readonly ICompanyDAL _companyDAL;
        private readonly IMapper _mapper;
        public CompanyManager(ICompanyDAL companyDAL,IMapper mapper)
        {
            _companyDAL = companyDAL;   
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


            _companyDAL.Add(company);
            _companyDAL.SaveChanges();
            return new SuccessResult(DefaultConstantValues.DATA_ADDED_SUCCESSFULLY);
        }

        public IResult Delete(int id)
        {
            var deleteEntity = _companyDAL.GetById(x => x.ID == id);

            deleteEntity.Deleted = id;
            _companyDAL.Update(deleteEntity);
            _companyDAL.SaveChanges();
            return new SuccessResult(DefaultConstantValues.DATA_DELETED_SUCCESSFULLY);
        }

        public IDataResult<Company> Get(int id)
        {
            return new SuccessDataResult<Company>(_companyDAL.GetById(x => x.ID == id && x.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE));
        }

        public IDataResult<List<CompanyListDTO>> GetAll()
        {
            List<Company> companies = _companyDAL.GetAll(x => x.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE);
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


            _companyDAL.Update(company);
            _companyDAL.SaveChanges();
            return new SuccessResult(DefaultConstantValues.DATA_ADDED_SUCCESSFULLY);
        }


        private IResult CheckDuplicateRow(Company model)
        {
            var allData = _companyDAL.GetAll(x => x.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE);
            var existingRow = allData.FirstOrDefault(x => x.Name == model.Name);
            if (existingRow != null)
                return new ErrorResult(DefaultConstantValues.DUPLICATE_RECORD_FOUND);

            return new SuccessResult();
        }
    }
}
