using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Messages
{
    public static class DepartmentMessage
    {
        internal readonly static string DepartmentNameMinLength = "Name must be minumum 3 symbols";
        internal readonly static string DepartmentNameMaxLength = "Name must be maximum 50 symbols";
        internal readonly static string DepartmentNameNoEmpty = "Name can not be empty";

        public static readonly string DepartmentAddedSuccesfully = "DepartmentAddedSuccesfully";
        public static readonly string DepartmentDeletedSuccesfully = "DepartmentDeletedSuccesfully";
        public static readonly string DepartmentUpdateSuccesfully = "DepartmentUpdateSuccesfully";
    }
}
