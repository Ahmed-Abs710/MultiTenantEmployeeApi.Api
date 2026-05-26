using MediatR;
using MultiTenantEmployeeApi.Application.Models;
using MultiTenantEmployeeApi.Domain.Exceptions;
using MultiTenantEmployeeApi.Infrastructure.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Application.Commands.DeleteEmployee
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IUnitOfWork _uow;

        public DeleteEmployeeHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var emp = await _uow.Employees.GetByIdAsync(request.Id);

            if (emp == null)
                throw new NotFoundException("عفوا العنصر غير موجود");

            _uow.Employees.Remove(emp);

            await _uow.SaveChangesAsync();

            return true;
        }
    }
}
