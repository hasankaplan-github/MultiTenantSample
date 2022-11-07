using MultiTenantSample.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSample.Application.Contracts;
public interface ISomeService
{
    SomeDataDto GetSomeData();
}
