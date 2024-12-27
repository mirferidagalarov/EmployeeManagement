using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using DataAccess.DatabaseContext;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class CompanyEfDAL:RepositoryBase<Company,DataContext>,ICompanyDAL
    {
        private readonly DataContext _context;
        public CompanyEfDAL(DataContext context):base(context)
        {
            _context = context;
        }
    }
}
