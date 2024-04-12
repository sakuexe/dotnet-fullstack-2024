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

        if(!AuthenticateUser(login.Username))
        {
            ModelState.AddModelError("validation", "Failed to authenticate user, try again. If the problem persists, contact the administrator.");
            return View(login);
        }

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        // remove the cookie
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
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

        if(!AuthenticateUser(newAccount.Username, newAccount.Email))
        {
            ModelState.AddModelError("validation", "Failed to authenticate user");
            return View(newAccount);
        }

        return RedirectToAction("Index", "Home");
    }

    private bool AuthenticateUser(string username, string? email = null, int expiresMinutes = 30)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        if (user == null)
            return false;

        var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
		};

        if (user.IsAdmin)
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));

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
        return true;
    }

}
