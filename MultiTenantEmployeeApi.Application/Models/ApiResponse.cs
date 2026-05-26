using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Application.Models
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }

        public object? Pagination { get; set; }

        public string? Error { get; set; }

        public bool Success => string.IsNullOrEmpty(Error);

        public static ApiResponse<T> SuccessResult(T data)
            => new() { Data = data };

        public static ApiResponse<T> Fail(string error)
            => new() { Error = error };
    }
}
