using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MultiTenantSample.Domain;

namespace MultiTenantSample.Infra.Db.SampleDbContext.EntityTypeConfigurations;

public class BaseEntityTypeConfiguration<TEntity> : Haskap.DddBase.Infra.Db.EntityTypeConfigurations.BaseEntityTypeConfiguration<TEntity>
    where TEntity : class
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
        if (typeof(IEntity).IsAssignableFrom(typeof(TEntity)))
        {
            builder.Property(x => (x as IEntity).Id).ValueGeneratedNever();
        }


        //if (typeof(IHasClusteredIndex).IsAssignableFrom(typeof(TEntity)))
        //{
        //    builder.HasKey(x => x.Id).IsClustered(false);
        //    builder.HasIndex(x => (x as IHasClusteredIndex).ClusteredIndex).IsClustered();
        //}
    }
}
