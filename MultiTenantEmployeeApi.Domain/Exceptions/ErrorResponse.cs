using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Domain.Exceptions
{
    public class ErrorResponse
    {
        public bool Success { get; set; } = false;

        public string Error { get; set; } = null!;

        public object? Errors { get; set; }
    }
}
