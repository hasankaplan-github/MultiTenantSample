using Haskap.DddBase.Domain.Providers;
using Haskap.DddBase.Domain.TenantAggregate;
using Haskap.DddBase.Infra.Db.Contexts.NpgsqlDbContext;
using Microsoft.EntityFrameworkCore;
using MultiTenantSample.Domain;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MultiTenantSample.Infra.Db.SampleDbContext;
public class AppDbContext : BaseEfCoreNpgsqlDbContext
{
    private readonly ICurrentTenantProvider _currentTenantProvider;
    private Guid? _currentTenantId => _currentTenantProvider.CurrentTenantId;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        ICurrentTenantProvider currentTenantProvider)
        : base(options)
    {
        _currentTenantProvider = currentTenantProvider;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        builder.Entity<SomeTenantDataClass>().HasQueryFilter(x => x.TenantId == _currentTenantId);
        builder.Entity<User>().HasQueryFilter(x => x.TenantId == _currentTenantId);

        base.OnModelCreating(builder);
    }

    public DbSet<SomeTenantDataClass> SomeTenantDataClass { get; set; }
    public DbSet<Tenant> Tenant { get; set; }
    public DbSet<User> User { get; set; }
}
