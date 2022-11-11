using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSample.Ui.MvcWebUi.Middlewares;
public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseIsActive(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<IsActiveMiddleware>();
    }
}
