using Microsoft.EntityFrameworkCore;
using MultiTenantEmployeeApi.Domain.Common.Interfaces;
using MultiTenantEmployeeApi.Domain.Entities;
using MultiTenantEmployeeApi.Infrastructure.Multitenancy;
using MultiTenantEmployeeApi.Infrastructure.Persistence.Configurations.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
       DbContextOptions<ApplicationDbContext> options, ICurrentTenant tenant)
       : base(options)
        {
            Tenant = tenant;
        }
        public DbSet<Employee> Employees => Set<Employee>();

        public DbSet<Tenant> Tenants => Set<Tenant>();

        //public DbSet<TaskItem> Tasks => Set<TaskItem>();

        public ICurrentTenant Tenant { get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            builder.Entity<Employee>()
                 .HasQueryFilter(x => x.TenantId == Tenant.TenantId && !x.IsDeleted);

            //builder.Entity<TaskItem>()
            //     .HasQueryFilter(x => x.TenantId == Tenant.TenantId && !x.IsDeleted);

            builder.Entity<Tenant>()
                .HasQueryFilter(x => !x.IsDeleted);

            builder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);

            //builder.ApplyGlobalFilters(this);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditing();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            ApplyAuditing();

            return base.SaveChanges();
        }

        private void ApplyAuditing()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                

                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is ITenantEntity tenantEntity)
                    {
                        tenantEntity.TenantId = Tenant.TenantId;
                    }

                    if (entry.Entity is IAuditableEntity auditable)
                    {
                        auditable.CreatedAt = DateTime.UtcNow;
                        auditable.UpdatedAt = DateTime.UtcNow;
                    }
                }

                if (entry.State == EntityState.Modified)
                {
                   

                    if (entry.Entity is IAuditableEntity auditable)
                    {
                        auditable.UpdatedAt = DateTime.UtcNow;
                    }
                }

            }
        }

    }
}
