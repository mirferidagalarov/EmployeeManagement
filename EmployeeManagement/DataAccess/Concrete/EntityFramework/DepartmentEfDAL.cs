using Core.Constants;
using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using DataAccess.DatabaseContext;
using Entities.Concrete.DTOs.DepartmentDTOs;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class DepartmentEfDAL : RepositoryBase<Department, DataContext>, IDepartmentDAL
    {
        private readonly DataContext _context;
        public DepartmentEfDAL(DataContext context) : base(context)
        {
            _context = context;
        }

        public List<DepartmentListDTO> GetAllDepartmentCompany()
        {
            var result = from dt in _context.Departments
                         where dt.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE
                         join cy in _context.Companies on dt.CompanyId equals cy.ID
                         where cy.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE
                         select new DepartmentListDTO
                         {
                             ID = dt.ID,
                             Name = dt.Name,
                             CompanyName = cy.Name,
                             CompanyId = cy.ID,
                         };

            return result.ToList();
        }

        public DepartmentListDTO GetDepartmentCompany(int id)
        {
            var result = from dt in _context.Departments
                         where dt.ID==id && dt.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE
                         join cy in _context.Companies on dt.CompanyId equals cy.ID
                         where cy.Deleted == DefaultConstantValues.DEFAULT_DELETED_COLUMN_VALUE
                         select new DepartmentListDTO
                         {
                             ID = dt.ID,
                             Name = dt.Name,
                             CompanyName = cy.Name,
                             CompanyId = cy.ID,
                         };

            return result.FirstOrDefault();
        }
    }
}
