using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using restingapi.Models;

namespace restingapi.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private static readonly List<Credentials> _users = new();

    public LoginController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        if (User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost]
    public IActionResult Index(Credentials model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }

        // look for username and password in the list of users
        if (_users.Any(u => u.Username == model.Username && u.Password != model.Password))
        {
            // add an error to Username field
            ModelState.AddModelError("Username", "User with that name already exists");
            return View(model);
        }

        _users.Add(model);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.Username),
            // the time and date for the login
            new Claim(ClaimTypes.UserData, new DateTime().ToString()),
            // a randomized session id
            new Claim(ClaimTypes.UserData, new Random().Next().ToString()),
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            IssuedUtc = DateTimeOffset.UtcNow,
            IsPersistent = true,
        };

        HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties
        );

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}
