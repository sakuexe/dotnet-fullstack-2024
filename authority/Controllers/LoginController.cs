using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using authority.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace authority.Controllers;

public class LoginController : Controller
{
	[HttpGet]
	public IActionResult Index()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Index(string name)
	{
		if (!string.IsNullOrEmpty(name))
			Login(name);
		return RedirectToAction("Index");
	}
	private void Login(string name)
	{
		var claims = new List<Claim> {
			new Claim(ClaimTypes.Name, name),
			new Claim(ClaimTypes.UserData, "I am a user!"),
		};

		if (name == "admin")
			claims.Add(new Claim(ClaimTypes.Role, "Admin"));

		var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

		var authProperties = new AuthenticationProperties
		{
			// if user is still on site, the cookie will be refreshed
			AllowRefresh = true,
			ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
			// does persist even after the browser is closed
			IsPersistent = true,
		};

		HttpContext.SignInAsync(
			CookieAuthenticationDefaults.AuthenticationScheme,
			new ClaimsPrincipal(claimsIdentity),
			authProperties
		);
	}

	public IActionResult Logout()
	{
		// remove the cookie
		HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		return RedirectToAction("Index");
	}
}