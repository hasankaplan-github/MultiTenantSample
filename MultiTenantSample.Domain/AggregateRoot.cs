using Haskap.DddBase.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSample.Domain;

public abstract class AggregateRoot : AggregateRoot<Guid>, IEntity
{
    protected AggregateRoot()
    {

    }

    protected AggregateRoot(Guid id)
        : base(id)
    {

    }
}