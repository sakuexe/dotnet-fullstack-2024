using forming.Models;
using Microsoft.AspNetCore.Mvc;

namespace forming.Controllers
{
	public class RegisterController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Index(RegisterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);  // return the invalid model back to frontend
			}
			return RedirectToAction("Index", "Home");
		}
	}
}