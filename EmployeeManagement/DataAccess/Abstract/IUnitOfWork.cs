using Core.DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUnitOfWork
    {
        ICompanyDAL CompanyDAL { get; }
        IDepartmentDAL DepartmentDAL { get; }
        IEmployeeDAL EmployeeDAL { get; }
        Task<int> SaveAsync();
    }
}
