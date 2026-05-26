using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using MultiTenantEmployeeApi.Domain.Common.Interfaces;
using MultiTenantEmployeeApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Infrastructure.Persistence.Configurations
{
    public class AuditInformation
    {
        //private void ApplyTenantInformation()
        //{
        //    foreach (var entry in ChangeTracker.Entries<ITenantEntity>())
        //    {
        //        if (entry.State == EntityState.Added)
        //        {
        //            entry.Entity.TenantId = Tenant.TenantId;
        //        }
        //    }
        //}
    }
}
