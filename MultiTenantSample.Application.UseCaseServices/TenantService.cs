using MultiTenantSample.Application.Contracts;
using MultiTenantSample.Application.Dtos;
using MultiTenantSample.Infra.Db.SampleDbContext;

namespace MultiTenantSample.Application.UseCaseServices;
public class TenantService : ITenantService
{
    private readonly AppDbContext _appDbContext;

    public TenantService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public List<TenantDto> GetAllTenants()
    {
        var tenantDtos = _appDbContext.Tenant
            .Select(x => new TenantDto
            {
                TenantId = x.Id,
                TenantName = x.Name
            })
            .ToList();

        return tenantDtos;
    }

    public TenantDto GetTenantById(Guid tenantId)
    {
        var tenantDto = _appDbContext.Tenant
            .Where(x => x.Id == tenantId)
            .Select(x => new TenantDto
            {
                TenantId = x.Id,
                TenantName = x.Name
            })
            .FirstOrDefault();

        return tenantDto;
    }
}
