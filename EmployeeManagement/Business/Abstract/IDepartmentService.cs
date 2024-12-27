using Core.Results.Abstract;
using Entities.Concrete.DTOs.CompanyDTOs;
using Entities.Concrete.DTOs.DepartmentDTOs;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDepartmentService
    {
        IResult Add(DepartmentAddDTO  departmentAddDTO);
        IResult Update(DepartmentUpdateDTO departmentUpdateDTO);
        IResult Delete(int id);
        IDataResult<List<DepartmentListDTO>> GetAllDepartmentCompany();
        IDataResult<DepartmentListDTO> GetDepartmentCompany(int id);
    }
}
