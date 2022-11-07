using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MultiTenantSample.Application.Contracts;
using MultiTenantSample.Application.Dtos;
using Haskap.DddBase.Domain.TenantAggregate;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MultiTenantSample.Ui.MvcWebUi.Controllers;
public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly ITenantService _tenantService;

    public AccountController(
        IAccountService accountService, 
        ITenantService tenantService)
    {
        _accountService = accountService;
        _tenantService = tenantService;
    }

    public IActionResult Login()
    {
        var tenantDtos = _tenantService.GetAllTenants();
        ViewBag.Tenants = tenantDtos.Select(x => new SelectListItem
            {
                Text = x.TenantName,
                Value = x.TenantId.ToString()
            })
            .ToList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginInputDto inputDto, CancellationToken cancellationToken)
    {
        try
        {
            var output = _accountService.Login(inputDto);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, inputDto.Username),
                new Claim(ClaimTypes.NameIdentifier, output.UserId.ToString()),
                new Claim(Tenant.ClaimKey, output.TenantId.Value.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = false,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                IssuedUtc = DateTimeOffset.UtcNow
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            return View();
        }
    }
}
