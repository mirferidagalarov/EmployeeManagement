using AutoMapper;
using Entities.Concrete.DTOs.CompanyDTOs;
using Entities.Concrete.DTOs.DepartmentDTOs;
using Entities.Concrete.DTOs.EmployeeDTOs;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mappers
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            #region CompanyMapper
            CreateMap<Company, CompanyListDTO>().ReverseMap();
            CreateMap<CompanyAddDTO, Company>().ReverseMap();
            CreateMap<CompanyUpdateDTO, Company>().ReverseMap();
            #endregion

            #region DepartmentMapper
            CreateMap<Department, DepartmentListDTO>().ReverseMap();
            CreateMap<DepartmentAddDTO, Department>().ReverseMap();
            CreateMap<DepartmentUpdateDTO, Department>().ReverseMap();
            #endregion

            #region EmployeeMapper
            CreateMap<Employee, EmployeeListDTO>().ReverseMap();
            CreateMap<EmployeeAddDTO, Employee>().ReverseMap();
            CreateMap<EmployeeUpdateDTO, Employee>().ReverseMap();
            #endregion
        }
    }
}
