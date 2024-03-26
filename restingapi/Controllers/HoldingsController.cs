using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using restingapi.Models;

namespace restingapi.Controllers;

public class HoldingsController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private static readonly List<UserHoldings> _users = new List<UserHoldings>();

    public HoldingsController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Index()
    {
        if (!User.Identity!.IsAuthenticated)
        {
            // if user is not logged in, return a partial view with a message
            return PartialView("_NotLoggedIn");
        }
        var identity = HttpContext.User.Identity;
        var username = identity!.Name!;

        var viewModel = new MyStonksViewModel(username);
        var usersHoldings = _users.Find(u => u.Username == username);
        // if user is not already in the list, create a new UserHoldings object
        // do not save it to the list yet, only after the first purchase is made
        usersHoldings ??= new UserHoldings(username);

        // if the user has no holdings, return a partial view with a message
        if (usersHoldings.Stonks.Count == 0)
            return PartialView("_NoStonks");

        // add the user's holdings to the view model
        viewModel.UserHoldings = usersHoldings;
        return PartialView("_MyStonks", viewModel);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public IActionResult BuyStonk(string business, int value)
    {
        var identity = HttpContext.User.Identity;
        var username = identity?.Name;

        if (username == null)
            return RedirectToAction("Index", "Home");
        if (string.IsNullOrWhiteSpace(business))
            return BadRequest("No business name provided");
        if (value <= 0)
            return BadRequest($"Invalid purchase price: {value}. Expected Int reprecenting cents");

        // search for the user in the list
        var usersHoldings = _users.Find(u => u.Username == username);
        // if not found, create a new UserHoldings object
        usersHoldings ??= new UserHoldings(username);

        // add a purchase to the user's holdings
        usersHoldings.AddStonk(business, value);

        _users.Remove(usersHoldings);
        _users.Add(usersHoldings);

        // add query string to redirect to the index page
        return RedirectToAction("Index", "Home");
    }
}
