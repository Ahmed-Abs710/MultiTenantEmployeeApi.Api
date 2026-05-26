using MultiTenantEmployeeApi.Domain.Entities;
using MultiTenantEmployeeApi.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Infrastructure.Persistence.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Employee> Employees { get; }

        //IRepository<TaskItem> Tasks { get; }

        IRepository<Tenant> Tenants { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
