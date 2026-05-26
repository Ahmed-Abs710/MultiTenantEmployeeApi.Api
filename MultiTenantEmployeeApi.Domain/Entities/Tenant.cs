using MultiTenantEmployeeApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Domain.Entities
{
    public class Tenant : Entity<Guid>
    {
        public string Name { get; set; } = default!;

        public ICollection<Employee> Employees { get; set; }
            = new List<Employee>();
    }
}
