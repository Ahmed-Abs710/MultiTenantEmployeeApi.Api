using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantEmployeeApi.Domain.Common;
using MultiTenantEmployeeApi.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Infrastructure.Persistence.Configurations
{
    public abstract class BaseEntityConfiguration<TEntity, TKey>
     : IEntityTypeConfiguration<TEntity>
     where TEntity : class, IEntity<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.ID);
            builder.ApplyCommonConfigurations();
        }
    }
}
