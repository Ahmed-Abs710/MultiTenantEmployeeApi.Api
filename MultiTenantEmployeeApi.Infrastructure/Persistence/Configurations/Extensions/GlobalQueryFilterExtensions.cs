using Microsoft.EntityFrameworkCore;
using MultiTenantEmployeeApi.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Infrastructure.Persistence.Configurations.Extensions
{
    public static class GlobalQueryFilterExtensions
    {
        public static void ApplyGlobalFilters(
            this ModelBuilder builder,
            ApplicationDbContext context)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;

                var parameter = Expression.Parameter(clrType, "e");

                Expression? filter = null;

                // --------------------------
                // Soft Delete Filter
                // --------------------------
                if (typeof(ISoftDelete).IsAssignableFrom(clrType))
                {
                    var isDeletedProperty = Expression.Property(
                        parameter,
                        nameof(ISoftDelete.IsDeleted));

                    var isNotDeleted = Expression.Equal(
                        isDeletedProperty,
                        Expression.Constant(false));

                    filter = isNotDeleted;
                }

                // --------------------------
                // Tenant Filter
                // --------------------------
                if (typeof(ITenantEntity).IsAssignableFrom(clrType))
                {
                    var tenantProperty = Expression.Property(
                        parameter,
                        nameof(ITenantEntity.TenantId));

                    var currentTenantId = Expression.Property(
                        Expression.Constant(context.Tenant),
                        nameof(context.Tenant.TenantId));

                    var tenantCondition = Expression.Equal(
                        tenantProperty,
                        currentTenantId);

                    filter = filter == null
                        ? tenantCondition
                        : Expression.AndAlso(filter, tenantCondition);
                }

                // --------------------------
                // Apply Filter
                // --------------------------
                if (filter != null)
                {
                    var lambda = Expression.Lambda(filter, parameter);

                    builder.Entity(clrType)
                        .HasQueryFilter(lambda);
                }
            }
        }
    }
}
