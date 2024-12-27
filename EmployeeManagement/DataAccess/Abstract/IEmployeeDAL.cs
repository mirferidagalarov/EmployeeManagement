using Core.DataAccess.Abstract;
using Entities.Concrete.DTOs.EmployeeDTOs;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IEmployeeDAL:IRepository<Employee>
    {
        public List<EmployeeListDTO> GetAllEmployee();
        public EmployeeListDTO GetEmployee(int id); 
        public List<EmployeeListDTO> EmployeeSearch (string text);
        public int EmployeeSearchCount(string text);

    }
}
