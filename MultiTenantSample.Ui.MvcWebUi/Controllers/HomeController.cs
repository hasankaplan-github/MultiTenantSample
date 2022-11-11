using Haskap.DddBase.Domain;
using Haskap.DddBase.Domain.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiTenantSample.Application.Contracts;
using MultiTenantSample.Domain;
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
    private readonly IGlobalQueryFilterParameterStatusCollectionProvider _globalQueryFilterParameterStatusCollectionProvider;

    public HomeController(
        ILogger<HomeController> logger,
        ISomeService someService,
        ITenantService tenantService,
        ICurrentTenantProvider currentTenantProvider,
        IGlobalQueryFilterParameterStatusCollectionProvider globalQueryFilterParameterStatusCollectionProvider)
    {
        _logger = logger;
        _someService = someService;
        _tenantService = tenantService;
        _currentTenantProvider = currentTenantProvider;
        _globalQueryFilterParameterStatusCollectionProvider = globalQueryFilterParameterStatusCollectionProvider;
    }

    public IActionResult Index()
    {
        ViewBag.CurrentTenantDto = _tenantService.GetTenantById(_currentTenantProvider.CurrentTenantId ?? Guid.Empty);
        ViewBag.SomeDataDto = _someService.GetSomeData();
        using (_globalQueryFilterParameterStatusCollectionProvider.Disable<IHasMultiTenant>())
        {
            ViewBag.SomeDataCountWithoutMt = _someService.GetSomeDataCount();
        }
        ViewBag.SomeDataCount = _someService.GetSomeDataCount();
        using (_globalQueryFilterParameterStatusCollectionProvider.Disable<ISoftDeletable>())
        {
            ViewBag.SomeDataCountWithDeleted = _someService.GetSomeDataCount();
            using (_globalQueryFilterParameterStatusCollectionProvider.Disable<IHasMultiTenant>())
            {
                ViewBag.SomeDataCountWithDeletedAndWithoutMt = _someService.GetSomeDataCount();
                using (_globalQueryFilterParameterStatusCollectionProvider.Disable<IIsActive>())
                {
                    ViewBag.SomeDataCountWithDeletedAndWithoutMtAndIsNotActive = _someService.GetSomeDataCount();
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
