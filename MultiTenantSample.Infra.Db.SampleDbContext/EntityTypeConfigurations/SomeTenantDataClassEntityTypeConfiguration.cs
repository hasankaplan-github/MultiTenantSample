using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantSample.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSample.Infra.Db.SampleDbContext.EntityTypeConfigurations;
public class SomeTenantDataClassEntityTypeConfiguration : BaseEntityTypeConfiguration<SomeTenantDataClass>
{
    public override void Configure(EntityTypeBuilder<SomeTenantDataClass> builder)
    {
        base.Configure(builder);
    }
}
