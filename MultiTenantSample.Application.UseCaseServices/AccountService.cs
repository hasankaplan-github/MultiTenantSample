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
    private readonly ITenantService _tenantService;
    private readonly ICurrentTenantProvider _currentTenantProvider;

    public AccountService(
        AppDbContext appDbContext,
        ITenantService tenantService,
        ICurrentTenantProvider currentTenantProvider)
    {
        _appDbContext = appDbContext;
        _tenantService = tenantService;
        _currentTenantProvider = currentTenantProvider;
    }

    public LoginOutputDto Login(LoginInputDto loginInputDto)
    {
        using (_currentTenantProvider.ChangeCurrentTenant(loginInputDto.TenantId))
        {
            var user = _appDbContext.User
                        .Where(x => x.Username == loginInputDto.Username && x.Password == loginInputDto.Password)
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
}
