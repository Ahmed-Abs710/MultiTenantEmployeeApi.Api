using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantEmployeeApi.Domain.Common.Interfaces;
using MultiTenantEmployeeApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Infrastructure.Persistence.Configurations
{
    public static class EntityConfigurationExtensions
    {
        public static void ApplyCommonConfigurations<TEntity>(
        this EntityTypeBuilder<TEntity> builder)
        where TEntity : class
        {
            // ----------------------
            // Tenant
            // ----------------------
            if (typeof(ITenantEntity).IsAssignableFrom(typeof(TEntity)))
            {
                builder.Property(nameof(ITenantEntity.TenantId))
                    .IsRequired();

                builder.HasIndex(nameof(ITenantEntity.TenantId));

            }

            // ----------------------
            // Soft Delete
            // ----------------------
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                builder.Property(nameof(ISoftDelete.IsDeleted))
                    .HasDefaultValue(false);

                builder.HasIndex(nameof(ISoftDelete.IsDeleted));
            }

            // ----------------------
            // Audit
            // ----------------------
            if (typeof(IAuditableEntity).IsAssignableFrom(typeof(TEntity)))
            {
                builder.Property(nameof(IAuditableEntity.CreatedAt))
                    .IsRequired();

                //builder.Property(nameof(IAuditableEntity.CreatedBy))
                //    .HasMaxLength(100);

                //builder.Property(nameof(IAuditableEntity.UpdatedBy))
                //    .HasMaxLength(100);
            }

            // ----------------------
            // Concurrency
            // ----------------------
            //if (typeof(IConcurrencySafe).IsAssignableFrom(typeof(TEntity)))
            //{
            //    builder.Property(nameof(IConcurrencySafe.RowVersion))
            //        .IsRowVersion()
            //        .IsConcurrencyToken()
            //        .ValueGeneratedOnAddOrUpdate();
            //}
        }
    }
}
