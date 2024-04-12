using System.Security.Claims;
using financeapp.Data;
using financeapp.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace financeapp.Controllers;

public class LoginController : Controller
{
    private readonly FinancesContext _context;
    public LoginController(FinancesContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        // if the user is already logged in, redirect to the home page
        if (User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        ViewData["Title"] = "Login";
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken] // prevents CSRF attacks
    public IActionResult Index(LoginViewModel login)
    {
        if (!ModelState.IsValid)
        {
            return View(login);
        }

        if (!login.ValidateLogin(_context))
        {
            ModelState.AddModelError("Username", "User not found with given username and password");
            return View(login);
        }

        AuthenticateUser(login.Username);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        // remove the cookie
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        // if the user is already logged in, redirect to the home page
        if (User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        ViewData["Title"] = "Register";
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken] // prevents CSRF attacks
    public IActionResult Register(RegisterViewModel newAccount)
    {
        if (!ModelState.IsValid)
        {
            return View(newAccount);
        }

        var error = newAccount.SaveToDatabase(_context); // golang-esque error handling

        if (!string.IsNullOrEmpty(error))
        {
            ModelState.AddModelError("validation", error);
            return View(newAccount);
        }

        AuthenticateUser(newAccount.Username, newAccount.Email);

        return RedirectToAction("Index", "Home");
    }

    private void AuthenticateUser(string username, string? email = null, int expiresMinutes = 30)
    {
        var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Email, email ?? string.Empty),
		};

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            // if user is still on site, the cookie will be refreshed
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(expiresMinutes), 
            IsPersistent = true,
        };

        HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties
        );
    }

}
