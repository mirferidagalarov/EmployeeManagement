using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Validation
{
    public static class ValidationTool
    {
        public static bool Validate(IValidator validator, object entity, out List<ValidationErrorModel> errors)
        {
            errors = Enumerable.Empty<ValidationErrorModel>().ToList();
            ValidationErrorModel model = null;
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    model = new ValidationErrorModel();
                    model.ErrorMessage = error.ErrorMessage;
                    model.ErrorCode = error.ErrorCode;
                    errors.Add(model);
                }
            }

            return result.IsValid;
        }
    }
}
