using Core.DataAccess.Abstract;
using Entities.Concrete.DTOs.DepartmentDTOs;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IDepartmentDAL:IRepository<Department>
    {
        public List<DepartmentListDTO> GetAllDepartmentCompany();
        public DepartmentListDTO GetDepartmentCompany(int id); 
    }
}
