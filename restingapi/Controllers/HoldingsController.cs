using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using restingapi.Models;

namespace restingapi.Controllers;

public class HoldingsController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private static readonly List<UserHoldings> _users = new();

    public HoldingsController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Index()
    {
        if (!User.Identity!.IsAuthenticated)
        {
            return PartialView("_NotLoggedIn");
        }
        var identity = HttpContext.User.Identity;
        var username = identity!.Name!;
        /* Console.WriteLine(_users.Count); */
        var usersHoldings = _users.Find(u => u.Username == username);
        // if user is not already in the list, create a new UserHoldings object
        // do not save it to the list yet, only after the first purchase is made
        usersHoldings ??= new UserHoldings(username);
        return PartialView("_MyStonks", usersHoldings);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public IActionResult BuyStonk(string business, double value)
    {
        var identity = HttpContext.User.Identity;
        var username = identity?.Name;

        if (username == null)
            return RedirectToAction("Index", "Home");
        if (string.IsNullOrWhiteSpace(business))
            return BadRequest("No business name provided");
        if (value <= 0)
            return BadRequest("Invalid purchase price");

        // search for the user in the list
        var usersHoldings = _users.Find(u => u.Username == username);
        // if not found, create a new UserHoldings object
        usersHoldings ??= new UserHoldings(username);

        // add a purchase to the user's holdings
        var valueInCents = (int)(value * 100);
        usersHoldings.AddStonk(business, valueInCents);

        _users.Add(usersHoldings);

        return RedirectToAction("Index", "Home");
    }
}
