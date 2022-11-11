using Haskap.DddBase.Domain;
using Haskap.DddBase.Domain.Providers;
using Haskap.DddBase.Domain.TenantAggregate;
using Microsoft.AspNetCore.Http;
using MultiTenantSample.Domain;
using MultiTenantSample.Domain.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSample.Ui.MvcWebUi.Middlewares;
public class IsActiveMiddleware
{
    private readonly RequestDelegate _next;

    public IsActiveMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(
        HttpContext httpContext, 
        IGlobalQueryFilterParameterStatusCollectionProvider filterParameterStatusCollectionProvider,
        IIsActiveGlobalQueryFilterParameterStatusProvider isActiveGlobalQueryFilterParameterStatusProvider)
    {
        filterParameterStatusCollectionProvider.AddFilterParameterStatusProvider<IIsActive>(isActiveGlobalQueryFilterParameterStatusProvider);

        await _next(httpContext);
    }
}
