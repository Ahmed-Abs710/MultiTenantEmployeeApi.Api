using MediatR;
using MultiTenantEmployeeApi.Application.DTOs;
using MultiTenantEmployeeApi.Application.Models;
using MultiTenantEmployeeApi.Domain.Entities;
using MultiTenantEmployeeApi.Domain.Enums;
using MultiTenantEmployeeApi.Domain.Exceptions;
using MultiTenantEmployeeApi.Domain.ValueObjects;
using MultiTenantEmployeeApi.Infrastructure.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Application.Commands.CreateEmployee
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
    {
        private readonly IUnitOfWork _uow;

        public CreateEmployeeHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var exists = await _uow.Employees
                .AnyAsync(x => x.Email == request.Email);

            if (exists)
                throw new DuplicateException("الايميل موجود مسبقا");

            var Salary = new Money(request.SalaryAmountMinor, request.CurrencyCode);
            var employee = new Employee
            {
                ID = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Department = request.Department,
                Status = EmployeeStatus.Active,
                Salary = Salary
            };

            await _uow.Employees.AddAsync(employee);
            await _uow.SaveChangesAsync();

            return new EmployeeDto
            {
                ID = employee.ID,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Department = employee.Department,
                Status = employee.Status.ToString(),
                Salary = new Money(employee.Salary.AmountMinor, employee.Salary.CurrencyCode)
            };
        }
    }
}
