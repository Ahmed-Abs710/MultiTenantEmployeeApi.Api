using MediatR;
using MultiTenantEmployeeApi.Application.DTOs;
using MultiTenantEmployeeApi.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Application.Queries.GetEmployees
{
    public record GetEmployeesQuery(int PageNumber = 1,int PageSize = 10,string? Search = null) : IRequest<PagedResult<EmployeeDto>>;
}
