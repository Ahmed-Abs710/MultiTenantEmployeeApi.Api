using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantEmployeeApi.Infrastructure.Multitenancy;
using MultiTenantEmployeeApi.Infrastructure.Persistence;
using MultiTenantEmployeeApi.Infrastructure.Persistence.Repository;
using MultiTenantEmployeeApi.Infrastructure.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString(
                        "DefaultConnection"));
            });

            //services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services.AddScoped<ITenantStore, TenantStore>();
            services.AddScoped<ICurrentTenant, CurrentTenant>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
