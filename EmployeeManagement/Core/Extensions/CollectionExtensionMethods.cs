using Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class CollectionExtensionMethods
    {
        public static string ValidationErrorMessagesWithNewLine(this List<ValidationErrorModel> model)
        {
            StringBuilder sb = new();
            foreach (var error in model)
            {
                sb.Append(error.ErrorMessage);
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
