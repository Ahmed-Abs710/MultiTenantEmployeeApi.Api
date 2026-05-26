using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Domain.Exceptions
{
    public class DuplicateException : AppException
    {
        public DuplicateException(string message)
            : base(message, 409)
        {
        }
    }
}
