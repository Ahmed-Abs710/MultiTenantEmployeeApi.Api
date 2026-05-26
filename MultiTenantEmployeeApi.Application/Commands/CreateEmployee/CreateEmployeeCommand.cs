using MediatR;
using MultiTenantEmployeeApi.Application.DTOs;
using MultiTenantEmployeeApi.Application.Models;
using MultiTenantEmployeeApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Application.Commands.CreateEmployee
{
    public record CreateEmployeeCommand(
     string FirstName,
     string LastName,
     string Email,
     string Department,
     int SalaryAmountMinor,
     string CurrencyCode
 ) : IRequest<EmployeeDto>;
}
