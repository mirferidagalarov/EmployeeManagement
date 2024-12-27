using Core.Results.Abstract;
using Entities.Concrete.DTOs.CompanyDTOs;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICompanyService
    {
        IResult Add(CompanyAddDTO  companyAddDTO);
        IResult Update(CompanyUpdateDTO companyUpdateDTO);
        IResult Delete(int id);
        IDataResult<List<CompanyListDTO>> GetAll();
        IDataResult<Company> Get(int id);
    }
}
