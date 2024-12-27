using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Messages
{
    public static class CompanyMessage
    {
        internal readonly static string CompanyNameMinLength = "Name must be minumum 3 symbols";
        internal readonly static string CompanyNameMaxLength = "Name must be maximum 50 symbols";
        internal readonly static string CompanyNameNoEmpty = "Name can not be empty";

        public static readonly string CompanyAddedSuccesfully = "CompanyAddedSuccesfully";
        public static readonly string CompanyDeletedSuccesfully = "CompanyDeletedSuccesfully";
        public static readonly string CompanyUpdateSuccesfully = "CompanyUpdateSuccesfully";
    }
}
