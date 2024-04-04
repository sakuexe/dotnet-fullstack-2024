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
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken] // prevents CSRF attacks
    public IActionResult Index(LoginViewModel login)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("validation", "Invalid username or password");
            return View(login);
        }

        if (!login.ValidateLogin(_context))
        {
            Console.WriteLine("User not found with given username and password");
            ModelState.AddModelError("validation", "User not found with given username and password");
            return View(login);
        }

        var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, login.Username),
			/* new Claim(ClaimTypes.UserData, "I am a user!"), */
		};

        if (login.Username == "admin")
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            // if user is still on site, the cookie will be refreshed
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30), // cookie expires in 30 minutes
                                                               // does persist even after the browser is closed
            IsPersistent = true,
        };

        HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties
        );

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        // remove the cookie
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}
