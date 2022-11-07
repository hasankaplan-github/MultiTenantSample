using Haskap.DddBase.Domain.TenantAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantSample.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSample.Infra.Db.SampleDbContext.EntityTypeConfigurations;
public class UserEntityTypeConfiguration : BaseEntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
    }
}
