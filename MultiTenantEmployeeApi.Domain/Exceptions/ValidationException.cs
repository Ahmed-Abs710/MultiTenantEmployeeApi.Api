using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Domain.Exceptions
{
    public class ValidationException : AppException
    {
        public List<string> Errors { get; }

        public ValidationException(List<string> errors)
            : base("Validation failed", 400)
        {
            Errors = errors;
        }

        public ValidationException(string error)
           : base("Validation failed", 400)
        {
            Errors.Add(error);
        }
    }
}
