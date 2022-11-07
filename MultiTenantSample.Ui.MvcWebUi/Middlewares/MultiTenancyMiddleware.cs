using Haskap.DddBase.Domain.Providers;
using Haskap.DddBase.Domain.TenantAggregate;
using MultiTenantSample.Application.Contracts;

namespace MultiTenantSample.Ui.MvcWebUi.Middlewares;

public class MultiTenancyMiddleware
{
    private readonly RequestDelegate _next;
    private ITenantService _tenantService;

    public MultiTenancyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(
        HttpContext httpContext, 
        ICurrentTenantProvider currentTenantProvider, 
        ITenantService tenantService)
    {
        _tenantService = tenantService;
        using (currentTenantProvider.ChangeCurrentTenant(FindTenant(httpContext)))
        {
            await _next(httpContext);
        }
    }

    private Tenant? FindTenant(HttpContext httpContext)
    {
        var tenantIdString = FindFromClaims(httpContext) ??
                       FindFromDomain(httpContext) ??
                       FindFromHeader(httpContext) ??
                       FindFromCookie(httpContext);

        if (tenantIdString == null)
        {
            return Tenant.EmptyTenant;
        }

        var tenantId = Guid.Parse(tenantIdString);

        var currentTenantDto = _tenantService.GetTenantById(tenantId);
        var currentTenant = new Tenant(currentTenantDto.TenantId, currentTenantDto.TenantName);

        return currentTenant;
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
