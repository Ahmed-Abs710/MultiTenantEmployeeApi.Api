using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Domain.Common.Interfaces
{
    public interface IEntity<TKey>
    {
        TKey ID { get; set; }
    }
}
