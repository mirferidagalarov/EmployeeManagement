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
    public class CompanyValidation : AbstractValidator<Company>
    {
        public CompanyValidation()
        {
            RuleFor(x => x.Name)
                  .NotEmpty().WithMessage(CompanyMessage.CompanyNameNoEmpty)
                  .MaximumLength(50).WithMessage(CompanyMessage.CompanyNameMaxLength)
                  .MinimumLength(3).WithMessage(CompanyMessage.CompanyNameMinLength);
        }
    }
}
