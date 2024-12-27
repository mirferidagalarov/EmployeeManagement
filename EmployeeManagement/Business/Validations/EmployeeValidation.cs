using Business.Messages;
using Entities.Concrete.TableModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations
{
    public class EmployeeValidation : AbstractValidator<Employee>
    {
        public EmployeeValidation()
        {

            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage(EmployeeMessage.EmployeeNameNoEmpty)
                 .MaximumLength(50).WithMessage(EmployeeMessage.EmployeeNameMaxLength)
                 .MinimumLength(3).WithMessage(EmployeeMessage.EmployeeNameMinLength);

            RuleFor(x => x.Surname)
                 .NotEmpty().WithMessage(EmployeeMessage.EmployeeSurnameNoEmpty)
                 .MaximumLength(100).WithMessage(EmployeeMessage.EmployeeSurnameMaxLength)
                 .MinimumLength(3).WithMessage(EmployeeMessage.EmployeeSurnameMinLength);
        }
    }
}
