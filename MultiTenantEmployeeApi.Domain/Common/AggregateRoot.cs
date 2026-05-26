using MultiTenantEmployeeApi.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Domain.Common
{
    public class AggregateRoot<T> : FullAuditEntity<T>//, IConcurrencySafe
    {
      //  public byte[] RowVersion { get; set; } = default!;
    }
}
