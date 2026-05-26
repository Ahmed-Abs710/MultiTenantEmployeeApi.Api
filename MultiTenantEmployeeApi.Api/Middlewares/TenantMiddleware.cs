using MultiTenantEmployeeApi.Infrastructure.Multitenancy;

namespace MultiTenantEmployeeApi.Api.Middlewares
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ICurrentTenant currentTenant, ITenantStore tenantStore)
        {
            if (!context.Request.Headers.TryGetValue("X-Tenant-Id", out var tenantId))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Missing Tenant Id");
                return;
            }

            if (!Guid.TryParse(tenantId, out var parsedTenantId))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid Tenant Id");
                return;
            }
            var CurrentTenant = await tenantStore.GetCurrentTenant(parsedTenantId);
            currentTenant.SetCurrentTenant(CurrentTenant);

            //context.Items["TenantId"] = parsedTenantId;

            await _next(context);
        }
    }
}
