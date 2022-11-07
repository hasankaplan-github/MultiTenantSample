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
    private Guid? _tenantId => CurrentTenantProvider.CurrentTenantId;

    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        builder.Entity<SomeTenantDataClass>().HasQueryFilter(x => x.TenantId == _tenantId);
        builder.Entity<User>().HasQueryFilter(x => x.TenantId == _tenantId);

        base.OnModelCreating(builder);
    }

    public DbSet<SomeTenantDataClass> SomeTenantDataClass { get; set; }
    public DbSet<Tenant> Tenant { get; set; }
    public DbSet<User> User { get; set; }
}
