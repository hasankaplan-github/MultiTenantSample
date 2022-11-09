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
    private readonly IEfCoreGlobalQueryFilterParameterStatusProvider _efCoreGlobalQueryFilterParameterStatusProvider;

    public HomeController(
        ILogger<HomeController> logger,
        ISomeService someService,
        ITenantService tenantService,
        ICurrentTenantProvider currentTenantProvider,
        IEfCoreGlobalQueryFilterParameterStatusProvider efCoreGlobalQueryFilterParameterStatusProvider)
    {
        _logger = logger;
        _someService = someService;
        _tenantService = tenantService;
        _currentTenantProvider = currentTenantProvider;
        _efCoreGlobalQueryFilterParameterStatusProvider = efCoreGlobalQueryFilterParameterStatusProvider;
    }

    public IActionResult Index()
    {
        if (_efCoreGlobalQueryFilterParameterStatusProvider.MultiTenancyFilterIsEnabled)
        {
            ViewBag.CurrentTenantDto = _tenantService.GetTenantById(_currentTenantProvider.CurrentTenantId.Value);
            ViewBag.SomeDataDto = _someService.GetSomeData();
            using (_efCoreGlobalQueryFilterParameterStatusProvider.DisableMultiTenancyFilter())
            {
                ViewBag.SomeDataCountWithoutMt= _someService.GetSomeDataCount();
            }
            ViewBag.SomeDataCount = _someService.GetSomeDataCount();
            using (_efCoreGlobalQueryFilterParameterStatusProvider.DisableSoftDeleteFilter())
            {
                ViewBag.SomeDataCountWithDeleted = _someService.GetSomeDataCount();
                using (_efCoreGlobalQueryFilterParameterStatusProvider.DisableMultiTenancyFilter())
                {
                    ViewBag.SomeDataCountWithDeletedAndWithoutMt = _someService.GetSomeDataCount();
                }
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
