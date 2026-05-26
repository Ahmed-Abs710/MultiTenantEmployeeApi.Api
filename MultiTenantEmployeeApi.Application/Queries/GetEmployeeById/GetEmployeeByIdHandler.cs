using MediatR;
using Microsoft.EntityFrameworkCore;
using MultiTenantEmployeeApi.Application.DTOs;
using MultiTenantEmployeeApi.Application.Models;
using MultiTenantEmployeeApi.Domain.Entities;
using MultiTenantEmployeeApi.Domain.Exceptions;
using MultiTenantEmployeeApi.Infrastructure.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Application.Queries.GetEmployeeById
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
    {
        private readonly IUnitOfWork _uow;

        public GetEmployeeByIdHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var emp = await _uow.Employees
                .Query()
                .Where(x => x.ID == request.Id)
                //.Select(x => new EmployeeDto
                //{
                //    ID = x.ID,
                //    FirstName = x.FirstName,
                //    LastName = x.LastName,
                //    Email = x.Email,
                //    Department = x.Department,
                //    Status = x.Status.ToString(),
                //    SalaryAmountMinor = x.Salary.AmountMinor,
                //    CurrencyCode = x.Salary.CurrencyCode
                //})
                .FirstOrDefaultAsync();

            if (emp == null)
                throw new NotFoundException("عفوا العنصر غير موجود");

            return emp;
        }
    }
}
