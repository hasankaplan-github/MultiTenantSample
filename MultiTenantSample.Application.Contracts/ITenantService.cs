using MultiTenantSample.Application.Dtos;

namespace MultiTenantSample.Application.Contracts;
public interface ITenantService
{
    List<TenantDto> GetAllTenants();
    TenantDto GetTenantById(Guid tenantId);
}
