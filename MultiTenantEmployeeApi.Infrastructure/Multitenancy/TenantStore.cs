using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MultiTenantEmployeeApi.Domain.Entities;
using MultiTenantEmployeeApi.Domain.Exceptions;
using MultiTenantEmployeeApi.Infrastructure.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Infrastructure.Multitenancy
{
    internal class TenantStore : ITenantStore
    {
        public TenantStore(IMemoryCache cache, IUnitOfWork unitOfWork)
        {
            Cache = cache;
            UnitOfWork = unitOfWork;
        }

        public IMemoryCache Cache { get; }
        public IUnitOfWork UnitOfWork { get; }

        public async Task<Guid> GetCurrentTenant(Guid value)
        {
            var cacheKey = $"tenant:{value}";


            if (Cache.TryGetValue(cacheKey, out Guid tenantId))
            {
                return tenantId;
            }
            var tenant = await UnitOfWork.Tenants.Query().FirstOrDefaultAsync(x => x.ID == value);
            if (tenant == null)
                throw new NotFoundException("عفوا المستخدم غير موجود");

            Cache.Set(cacheKey, tenant.ID, TimeSpan.FromMinutes(30));
            return tenant.ID;
        }
    }
}
