using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MultiTenantEmployeeApi.Domain.Exceptions;
using MultiTenantEmployeeApi.Infrastructure.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Infrastructure.Multitenancy
{
    public class CurrentTenant : ICurrentTenant
    {
        public Guid TenantId{ get; private set;}


        public void SetCurrentTenant(Guid value)
        {
            TenantId = value;
        }
    }
}
