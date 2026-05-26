using FluentValidation;
using MultiTenantEmployeeApi.Application.Commands.CreateEmployee;
using MultiTenantEmployeeApi.Application.Commands.UpdateEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Application.Validators
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.Status)
                .IsInEnum();

        }
    }
}
