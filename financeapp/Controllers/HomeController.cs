using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using financeapp.Models;
using financeapp.Data;

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
        if (!User.Identity.IsAuthenticated)
        {
            // show the not logged in page from Home folder instead
            return View("NotLoggedIn");
        }
        var finances = new List<Finance>();
        using (var context = _context)
        {
            finances = context.Finances
                .Where(f => f.User.Username == User.Identity.Name).ToList();
        }
        return View(finances);
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
