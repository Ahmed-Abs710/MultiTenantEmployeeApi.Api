using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantEmployeeApi.Application.Commands.CreateEmployee;
using MultiTenantEmployeeApi.Application.Commands.DeleteEmployee;
using MultiTenantEmployeeApi.Application.Commands.UpdateEmployee;
using MultiTenantEmployeeApi.Application.Queries.GetEmployeeById;
using MultiTenantEmployeeApi.Application.Queries.GetEmployees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateEmployeeCommand).Assembly);

            return services;
        }
    }
}
