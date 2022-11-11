using Haskap.DddBase.Domain.Providers;
using MultiTenantSample.Application.UseCaseServices;
using MultiTenantSample.Application.Contracts;
using MultiTenantSample.Domain.Providers;
using MultiTenantSample.Infra.Providers;

namespace MultiTenantSample.Ui.MvcWebUi;

public static class ServiceCollectionExtensions
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        //services.AddTransient<PaymentCredentialsDomainService>();
    }

    public static void AddUseCaseServices(this IServiceCollection services)
    {
        services.AddTransient<ITenantService, TenantService>();
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<ISomeService, SomeService>();
    }

    public static void AddProviders(this IServiceCollection services)
    {
        //services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        //services.AddSingleton<IJwtProvider, JwtProvider>();
        services.AddScoped<IIsActiveGlobalQueryFilterParameterStatusProvider, IsActiveGlobalQueryFilterParameterStatusProvider>();
    }

    public static void AddEfInterceptors(this IServiceCollection services)
    {
        //services.AddScoped<AuditSaveChangesInterceptor<Guid?>>();
        //services.AddScoped<AuditHistoryLogSaveChangesInterceptor<Guid?>>();
    }
}