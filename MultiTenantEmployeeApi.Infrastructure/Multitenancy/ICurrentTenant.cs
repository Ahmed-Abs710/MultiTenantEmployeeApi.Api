using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Infrastructure.Multitenancy
{
    public interface ICurrentTenant
    {
        Guid TenantId { get; }
        void SetCurrentTenant(Guid value);
    }
}
