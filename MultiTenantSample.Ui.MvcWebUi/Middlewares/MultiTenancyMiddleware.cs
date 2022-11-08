using Haskap.DddBase.Domain.Providers;
using Haskap.DddBase.Domain.TenantAggregate;

namespace MultiTenantSample.Ui.MvcWebUi.Middlewares;

public class MultiTenancyMiddleware
{
    private readonly RequestDelegate _next;

    public MultiTenancyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(
        HttpContext httpContext,
        ICurrentTenantProvider currentTenantProvider)
    {
        //using (CurrentTenantProvider.ChangeCurrentTenant(FindTenant(httpContext)))
        //{
        //    await _next(httpContext);
        //}

        using (currentTenantProvider.ChangeCurrentTenant(FindTenant(httpContext)))
        {
            await _next(httpContext);
        }
    }

    private Guid? FindTenant(HttpContext httpContext)
    {
        var tenantIdString = FindFromClaims(httpContext) ??
                       FindFromDomain(httpContext) ??
                       FindFromHeader(httpContext) ??
                       FindFromCookie(httpContext);

        if (tenantIdString == null)
        {
            return Tenant.EmptyTenantId;
        }

        var tenantId = Guid.Parse(tenantIdString);

        return tenantId;
    }

    private string? FindFromClaims(HttpContext httpContext)
    {
        return httpContext.User.FindFirst(x=>x.Type == Tenant.ClaimKey)?.Value;
    }

    private string? FindFromDomain(HttpContext httpContext)
    {
        return null;
    }

    private string? FindFromHeader(HttpContext httpContext)
    {
        return httpContext.Request.Headers[Tenant.HeaderKey].FirstOrDefault();
    }

    private string? FindFromCookie(HttpContext httpContext)
    {
        return httpContext.Request.Cookies[Tenant.CookieKey];
    }
}
