using MultiTenantEmployeeApi.Domain.Entities;
using MultiTenantEmployeeApi.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Employees = new Repository<Employee>(_context);

            //Tasks = new Repository<TaskItem>(_context);

            Tenants = new Repository<Tenant>(_context);
        }

        public IRepository<Employee> Employees { get; }

        //public IRepository<TaskItem> Tasks { get; }

        public IRepository<Tenant> Tenants { get; }

        public async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
