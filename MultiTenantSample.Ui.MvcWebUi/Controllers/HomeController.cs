using Haskap.DddBase.Domain.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiTenantSample.Application.Contracts;
using MultiTenantSample.Models;
using System.Diagnostics;

namespace MultiTenantSample.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISomeService _someService;
    private readonly ITenantService _tenantService;
    private readonly ICurrentTenantProvider _currentTenantProvider;

    public HomeController(
        ILogger<HomeController> logger,
        ISomeService someService,
        ITenantService tenantService,
        ICurrentTenantProvider currentTenantProvider)
    {
        _logger = logger;
        _someService = someService;
        _tenantService = tenantService;
        _currentTenantProvider = currentTenantProvider;
    }

    public IActionResult Index()
    {
        if (_currentTenantProvider.MultiTenancyIsEnabled)
        {
            ViewBag.CurrentTenantDto = _tenantService.GetTenantById(_currentTenantProvider.CurrentTenantId.Value);
            ViewBag.SomeDataDto = _someService.GetSomeData();
            using (_currentTenantProvider.DisableMultiTenancy())
            {
                ViewBag.SomeDataCount = _someService.GetSomeDataCount();
            }
        }
        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
