using MediatR;
using MultiTenantEmployeeApi.Application.Models;
using MultiTenantEmployeeApi.Domain.Exceptions;
using MultiTenantEmployeeApi.Infrastructure.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Application.Commands.UpdateEmployee
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, bool>
    {
        private readonly IUnitOfWork _uow;

        public UpdateEmployeeHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var emp = await _uow.Employees.GetByIdAsync(request.Id);

            if (emp == null)
                throw new NotFoundException("عفوا العنصر غير موجود");

            emp.FirstName = request.FirstName;
            emp.LastName = request.LastName;
            emp.Department = request.Department;
            emp.Status = request.Status;
            emp.Salary.AmountMinor = request.SalaryAmountMinor;
            emp.Salary.CurrencyCode = request.CurrencyCode;

            _uow.Employees.Update(emp);

            await _uow.SaveChangesAsync();

            return true;
        }
    }
}
