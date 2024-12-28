using DataAccess.Abstract;
using DataAccess.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private CompanyEfDAL _companyEfDAL;
        private DepartmentEfDAL _departmentEfDAL;
        private EmployeeEfDAL _employeeEfDAL;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public ICompanyDAL CompanyDAL
        {
            get
            {
                if (_companyEfDAL == null)
                    _companyEfDAL = new CompanyEfDAL(_context);

                return _companyEfDAL;
            }
        }

        public IDepartmentDAL DepartmentDAL
        {
            get
            {
                if (_departmentEfDAL == null)
                    _departmentEfDAL = new DepartmentEfDAL(_context);

                return _departmentEfDAL;
            }
        }

        public IEmployeeDAL EmployeeDAL
        {
            get
            {
                if (_employeeEfDAL == null)
                    _employeeEfDAL = new EmployeeEfDAL(_context);

                return _employeeEfDAL;
            }
        }
        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
