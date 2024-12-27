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
    public class DepartmentValidation : AbstractValidator<Department>
    {
        public DepartmentValidation()
        {
            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage(DepartmentMessage.DepartmentNameNoEmpty)
                 .MaximumLength(50).WithMessage(DepartmentMessage.DepartmentNameMaxLength)
                 .MinimumLength(3).WithMessage(DepartmentMessage.DepartmentNameMinLength);
        }
    }
}
