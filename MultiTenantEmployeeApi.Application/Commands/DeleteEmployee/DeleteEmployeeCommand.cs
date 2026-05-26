using MediatR;
using MultiTenantEmployeeApi.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Application.Commands.DeleteEmployee
{
    public record DeleteEmployeeCommand(Guid Id)
    : IRequest<bool>;
}
