using MediatR;
using MultiTenantEmployeeApi.Application.DTOs;
using MultiTenantEmployeeApi.Application.Models;
using MultiTenantEmployeeApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Application.Queries.GetEmployeeById
{
    public record GetEmployeeByIdQuery(Guid Id)
    : IRequest<Employee>;
}
