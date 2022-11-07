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

    public HomeController(
        ILogger<HomeController> logger,
        ISomeService someService,
        ITenantService tenantService)
    {
        _logger = logger;
        _someService = someService;
        _tenantService = tenantService;
    }

    public IActionResult Index()
    {
        if (CurrentTenantProvider.CurrentTenantId.HasValue)
        {
            ViewBag.CurrentTenantDto = _tenantService.GetTenantById(CurrentTenantProvider.CurrentTenantId.Value);
            ViewBag.SomeDataDto = _someService.GetSomeData();
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
