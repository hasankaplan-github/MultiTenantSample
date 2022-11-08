using MultiTenantSample.Application.Contracts;
using MultiTenantSample.Application.Dtos;
using MultiTenantSample.Infra.Db.SampleDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSample.Application.UseCaseServices;
public class SomeService : ISomeService
{
    private readonly AppDbContext _appDbContext;

    public SomeService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public SomeDataDto GetSomeData()
    {
        return _appDbContext.SomeTenantDataClass
            .Select(x => new SomeDataDto
            {
                MyProperty = x.MyProperty
            })
            .First();
            
    }

    public int GetSomeDataCount()
    {
        return _appDbContext.SomeTenantDataClass
            .Count();
    }
}
