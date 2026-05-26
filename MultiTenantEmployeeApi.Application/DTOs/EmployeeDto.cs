using MultiTenantEmployeeApi.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Application.DTOs
{
    public class EmployeeDto
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Department { get; set; }
        public string Status { get; set; } = null!;
        //public int SalaryAmountMinor { get; set; }
        //public string CurrencyCode { get; set; } = null!;
        public Money Salary { get; set; } = null!;
    }
}
