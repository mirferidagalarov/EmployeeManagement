using Autofac;
using Business.Abstract;
using Business.Concrete;
using Business.Security;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependcyResolvers
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Company[...]
            builder.RegisterType<CompanyEfDAL>().As<ICompanyDAL>();
            builder.RegisterType<CompanyManager>().As<ICompanyService>();
            #endregion

            #region Department[...]
            builder.RegisterType<DepartmentEfDAL>().As<IDepartmentDAL>();
            builder.RegisterType<DepartmentManager>().As<IDepartmentService>();
            #endregion

            #region Employee[...]
            builder.RegisterType<EmployeeEfDAL>().As<IEmployeeDAL>();
            builder.RegisterType<EmployeeManager>().As<IEmployeeService>();
            #endregion

            #region User[...]
            builder.RegisterType<UsersManager>().As<IUserService>();
            #endregion

            #region Token[...]
            builder.RegisterType<TokenHandler>().As<ITokenHelper>();
            #endregion


        }
    }
}
