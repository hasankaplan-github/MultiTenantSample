using Haskap.DddBase.Domain;
using Haskap.DddBase.Domain.Providers;
using Haskap.DddBase.Domain.TenantAggregate;
using Haskap.DddBase.Infra.Db.Contexts.NpgsqlDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MultiTenantSample.Domain;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace MultiTenantSample.Infra.Db.SampleDbContext;
public class AppDbContext : BaseEfCoreNpgsqlDbContext
{
    private bool _isActiveFilterIsEnabled => _globalQueryFilterParameterStatusCollectionProvider.IsEnabled<IIsActive>();

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        ICurrentTenantProvider currentTenantProvider,
        IGlobalQueryFilterParameterStatusCollectionProvider globalQueryFilterParameterStatusCollectionProvider)
        : base(
            options, 
            currentTenantProvider,
            globalQueryFilterParameterStatusCollectionProvider)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(builder);
    }

    protected override bool ShouldFilterEntity<TEntity>(IMutableEntityType mutableEntityType)
    {
        if (typeof(IIsActive).IsAssignableFrom(typeof(TEntity)))
        {
            return true;
        }

        return base.ShouldFilterEntity<TEntity>(mutableEntityType);
    }

    protected override Expression<Func<TEntity, bool>>? CreateFilterExpression<TEntity>()
    {
        var expression = base.CreateFilterExpression<TEntity>();

        if (typeof(IIsActive).IsAssignableFrom(typeof(TEntity)))
        {
            Expression<Func<TEntity, bool>> isActiveFilterExpression =
                e => !_isActiveFilterIsEnabled || (e as IIsActive).IsActive == true; // EF.Property<bool>(e, "IsActive");
            expression = expression == null
                ? isActiveFilterExpression
                : CombineExpressions(expression, isActiveFilterExpression);
        }

        return expression;
    }

    public DbSet<SomeTenantDataClass> SomeTenantDataClass { get; set; }
    public DbSet<Tenant> Tenant { get; set; }
    public DbSet<User> User { get; set; }
}

/*
 protected bool IsActiveFilterEnabled => DataFilter?.IsEnabled<IIsActive>() ?? false;

protected override bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType)
{
    if (typeof(IIsActive).IsAssignableFrom(typeof(TEntity)))
    {
        return true;
    }

    return base.ShouldFilterEntity<TEntity>(entityType);
}

protected override Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
{
    var expression = base.CreateFilterExpression<TEntity>();

    if (typeof(IIsActive).IsAssignableFrom(typeof(TEntity)))
    {
        Expression<Func<TEntity, bool>> isActiveFilter =
            e => !IsActiveFilterEnabled || EF.Property<bool>(e, "IsActive");
        expression = expression == null 
            ? isActiveFilter 
            : CombineExpressions(expression, isActiveFilter);
    }

    return expression;
}
 */
