using Haskap.DddBase.Domain.Providers;
using Haskap.DddBase.Domain.TenantAggregate;
using MultiTenantSample.Application.Contracts;
using MultiTenantSample.Application.Dtos;
using MultiTenantSample.Domain;
using MultiTenantSample.Infra.Db.SampleDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSample.Application.UseCaseServices;
public class AccountService : IAccountService
{
    private readonly AppDbContext _appDbContext;
    private readonly ICurrentTenantProvider _currentTenantProvider;
    private readonly ITenantService _tenantService;

    public AccountService(
        AppDbContext appDbContext,
        ICurrentTenantProvider currentTenantProvider,
        ITenantService tenantService)
    {
        _appDbContext = appDbContext;
        _currentTenantProvider = currentTenantProvider;
        _tenantService = tenantService;
    }

    public LoginOutputDto Login(LoginInputDto loginInputDto)
    {
        var user = _appDbContext.User
            .Where(x => x.Username == loginInputDto.Username && x.Password == loginInputDto.Password && x.TenantId == loginInputDto.TenantId)
            .FirstOrDefault();

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        return new LoginOutputDto
        {
            UserId = user.Id,
            TenantId = user.TenantId
        };
    }
}
