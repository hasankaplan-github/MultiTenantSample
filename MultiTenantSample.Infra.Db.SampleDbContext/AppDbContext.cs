using Haskap.DddBase.Domain;
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
    //private readonly ICurrentTenantProvider _currentTenantProvider;
    //private Guid? _currentTenantId => _currentTenantProvider.CurrentTenantId;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        ICurrentTenantProvider currentTenantProvider)
        : base(options, currentTenantProvider)
    {
        //_currentTenantProvider = currentTenantProvider;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        //builder.Entity<SomeTenantDataClass>().HasQueryFilter(x => x.TenantId == _currentTenantId);
        //builder.Entity<User>().HasQueryFilter(x => x.TenantId == _currentTenantId);

        //var isSoftDeletable = false;
        //if (typeof(ISoftDeletable).IsAssignableFrom(typeof(SomeTenantDataClass)))
        //{
        //    isSoftDeletable = true;
        //    //builder.Property(x => (x as ISoftDeletable).IsDeleted).IsRequired();
        //    //builder.HasQueryFilter(x => (x as ISoftDeletable).IsDeleted == false);
        //}

        //var hasMultiTenant = false;
        //if (typeof(IHasMultiTenant).IsAssignableFrom(typeof(SomeTenantDataClass)))
        //{
        //    hasMultiTenant = true;
        //}

        //if (isSoftDeletable || hasMultiTenant)
        //{
        //    builder.Entity<SomeTenantDataClass>().HasQueryFilter(x =>
        //        (!isSoftDeletable || (x as ISoftDeletable).IsDeleted == false) &&
        //        (!hasMultiTenant || !_currentTenantProvider.MultiTenancyIsEnabled || x.TenantId == _currentTenantId));
        //    // CurrentTenant middleware içinde set edilmesi gerekiyor.
        //    // https://github.com/hikalkan/presentations/blob/master/2018-04-06-Multi-Tenancy/src/MultiTenancyDraft/Infrastructure/MultiTenancyMiddleware.cs
        //}


        /*
        var isSoftDeletable = false;
        if (typeof(ISoftDeletable).IsAssignableFrom(typeof(TEntity)))
        {
            isSoftDeletable = true;
            builder.Property(x => (x as ISoftDeletable).IsDeleted).IsRequired();
            //builder.HasQueryFilter(x => (x as ISoftDeletable).IsDeleted == false);
        }

        var hasMultiTenancy = false;
        if (typeof(IMultiTenant).IsAssignableFrom(typeof(TEntity)))
        {
            hasMultiTenancy = true;
        }

        if (isSoftDeletable || hasMultiTenancy)
        {
            builder.HasQueryFilter(x =>
                (!isSoftDeletable || (x as ISoftDeletable).IsDeleted == false) &&
                (!hasMultiTenancy || !CurrentTenantProvider.MultiTenancyIsEnabled || (x as IMultiTenant).TenantId == CurrentTenantProvider.CurrentTenant.Id));
            // CurrentTenant middleware içinde set edilmesi gerekiyor.
            // https://github.com/hikalkan/presentations/blob/master/2018-04-06-Multi-Tenancy/src/MultiTenancyDraft/Infrastructure/MultiTenancyMiddleware.cs
        }
        */

        base.OnModelCreating(builder);
    }

    public DbSet<SomeTenantDataClass> SomeTenantDataClass { get; set; }
    public DbSet<Tenant> Tenant { get; set; }
    public DbSet<User> User { get; set; }
}
