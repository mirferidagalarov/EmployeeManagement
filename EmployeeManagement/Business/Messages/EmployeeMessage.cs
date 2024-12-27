using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Messages
{
    public static class EmployeeMessage
    {
        internal readonly static string EmployeeNameMinLength = "Surname must be minumum 3 symbols";
        internal readonly static string EmployeeNameMaxLength = "Surname must be maximum 50 symbols";
        internal readonly static string EmployeeNameNoEmpty = "Surname can not be empty";

        internal readonly static string EmployeeSurnameMinLength = "Surname must be minumum 3 symbols";
        internal readonly static string EmployeeSurnameMaxLength = "Surname must be maximum 100 symbols";
        internal readonly static string EmployeeSurnameNoEmpty = "Surname can not be empty";

        public static readonly string EmployeeAddedSuccesfully = "EmployeeAddedSuccesfully";
        public static readonly string EmployeeDeletedSuccesfully = "EmployeeDeletedSuccesfully";
        public static readonly string EmployeeUpdateSuccesfully = "EmployeeUpdateSuccesfully";
    }
}
