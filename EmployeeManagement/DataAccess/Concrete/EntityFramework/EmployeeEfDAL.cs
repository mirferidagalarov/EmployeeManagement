using Core.Constants;
using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using DataAccess.DatabaseContext;
using Entities.Concrete.DTOs.EmployeeDTOs;
using Entities.Concrete.TableModels;

namespace DataAccess.Concrete.EntityFramework
{
    public class EmployeeEfDAL : RepositoryBase<Employee, DataContext>, IEmployeeDAL
    {
        private readonly DataContext _context;
        public EmployeeEfDAL(DataContext context) : base(context)
        {
            _context = context;
        }

        public List<EmployeeListDTO> EmployeeSearch(string text)
        {
            var result = from ep in _context.Employees
                         where ep.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE
                         join dt in _context.Departments on ep.DepartmenId equals dt.ID
                         where dt.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE
                         join cy in _context.Companies on dt.CompanyId equals cy.ID
                         where cy.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE && (ep.Name.ToLower().Contains(text.ToLower()) || ep.Surname.ToLower().Contains(text.ToLower()))
                         select new EmployeeListDTO
                         {
                             ID = ep.ID,
                             Name = ep.Name,
                             Surname = ep.Surname,
                             BirthDate = ep.BirthDate.Date,
                             DepartmenId = ep.DepartmenId,
                             DepartmentName = dt.Name,
                             CompanyName = cy.Name
                         };

            return result.ToList();
        }

        public int EmployeeSearchCount(string text)
        {
            var result = from ep in _context.Employees
                         where ep.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE
                         join dt in _context.Departments on ep.DepartmenId equals dt.ID
                         where dt.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE
                         join cy in _context.Companies on dt.CompanyId equals cy.ID
                         where cy.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE && (ep.Name.ToLower().Contains(text.ToLower()) || ep.Surname.ToLower().Contains(text.ToLower()))
                         select new EmployeeListDTO
                         {
                             ID = ep.ID,
                             Name = ep.Name,
                             Surname = ep.Surname,
                             BirthDate = ep.BirthDate.Date,
                             DepartmenId = ep.DepartmenId,
                             DepartmentName = dt.Name,
                             CompanyName = cy.Name
                         };

            return result.Count();
        }

        public List<EmployeeListDTO> GetAllEmployee()
        {
            var result = from ep in _context.Employees
                         where ep.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE
                         join dt in _context.Departments on ep.DepartmenId equals dt.ID
                         where dt.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE
                         join cy in _context.Companies on dt.CompanyId equals cy.ID
                         where cy.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE
                         select new EmployeeListDTO
                         {
                             ID = ep.ID,
                             Name = ep.Name,
                             Surname = ep.Surname,
                             BirthDate = ep.BirthDate.Date,
                             DepartmenId = ep.DepartmenId,
                             DepartmentName = dt.Name,
                             CompanyName = cy.Name
                         };

            return result.ToList();
        }

        public EmployeeListDTO GetEmployee(int id)
        {
            var result = from ep in _context.Employees
                         where ep.ID == id && ep.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE
                         join dt in _context.Departments on ep.DepartmenId equals dt.ID
                         where dt.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE
                         join cy in _context.Companies on dt.CompanyId equals cy.ID
                         where cy.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE
                         select new EmployeeListDTO
                         {
                             ID = ep.ID,
                             Name = ep.Name,
                             Surname = ep.Surname,
                             BirthDate = ep.BirthDate.Date,
                             DepartmenId = ep.DepartmenId,
                             DepartmentName = dt.Name,
                             CompanyName = cy.Name
                         };

            return result.FirstOrDefault();
        }
    }
}
