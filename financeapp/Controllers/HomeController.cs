using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using financeapp.Models;
using financeapp.Data;
using financeapp.Models.ViewModels;
using System.Text.Json;

namespace financeapp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly FinancesContext _context;

    public HomeController(ILogger<HomeController> logger, FinancesContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        ViewData["Title"] = "Dashboard";
        if (!User.Identity?.IsAuthenticated ?? User.Identity?.Name == null)
        {
            // show the not logged in page from Home folder instead
            return View("NotLoggedIn");
        }
        var dashboard = new DashboardViewModel(_context, User.Identity!.Name!);
        return View(dashboard);
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
