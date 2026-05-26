using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Domain.Exceptions
{
    public class ApiResponseWrapper<T>
    {
        public bool Success { get; set; } = true;

        public T? Data { get; set; }

        public string? Error { get; set; }
    }
}
