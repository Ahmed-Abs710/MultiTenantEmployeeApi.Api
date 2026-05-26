using MultiTenantEmployeeApi.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Domain.Common
{
    public abstract class Entity<T> : IEntity<T>, ISoftDelete
    {
        public T ID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        
    }
}
