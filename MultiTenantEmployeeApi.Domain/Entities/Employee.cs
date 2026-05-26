using MultiTenantEmployeeApi.Domain.Common;
using MultiTenantEmployeeApi.Domain.Enums;
using MultiTenantEmployeeApi.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Domain.Entities
{
    public class Employee : AggregateRoot<Guid>
    {

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string? Department { get; set; }

        public EmployeeStatus Status { get; set; }

        public string? CustomData { get; set; }

        public Money Salary { get; set; } = null!;

        public Tenant Tenant { get; set; } = default!;
    }
}
