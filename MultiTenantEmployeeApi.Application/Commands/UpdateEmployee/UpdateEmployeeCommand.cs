using MediatR;
using MultiTenantEmployeeApi.Application.Models;
using MultiTenantEmployeeApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Application.Commands.UpdateEmployee
{
    public record UpdateEmployeeCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Department,
    EmployeeStatus Status,
     int SalaryAmountMinor,
     string CurrencyCode
) : IRequest<bool>;
}
