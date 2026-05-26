using Microsoft.EntityFrameworkCore;
using MultiTenantEmployeeApi.Infrastructure.Persistence;

namespace MultiTenantEmployeeApi.Api.Extensions
{
    public static class MigrationExtensions
    {
        public static void MigrateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            db.Database.Migrate();
        }
    }
}
