using Haskap.DddBase.Infra.Providers;
using MultiTenantSample.Infra.Db.SampleDbContext;
using Microsoft.EntityFrameworkCore;
using MultiTenantSample.Ui.MvcWebUi;
using Microsoft.AspNetCore.Authentication.Cookies;
using Haskap.DddBase.Domain.Providers;
using Haskap.DddBase.Presentation.Middlewares;
using MultiTenantSample.Ui.MvcWebUi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBaseProviders<Guid>();
builder.Services.AddProviders();
builder.Services.AddUseCaseServices();

var connectionString = builder.Configuration.GetConnectionString("SampleConnectionString");
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    options.UseNpgsql(connectionString);
    options.UseSnakeCaseNamingConvention();
    //options.AddInterceptors(serviceProvider.GetRequiredService<AuditSaveChangesInterceptor<Guid?>>());
    //options.AddInterceptors(serviceProvider.GetRequiredService<AuditHistoryLogSaveChangesInterceptor<Guid?>>());
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
    });

builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSoftDelete();
app.UseMultiTenancy();
app.UseIsActive();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
