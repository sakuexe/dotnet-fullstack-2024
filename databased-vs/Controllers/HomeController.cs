using databased_vs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace databased_vs.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			var data = DatabaseManipulator.GetAll<User>("User");
			return View(data);
		}

		public IActionResult Privacy()
		{
			var testi = new User()
			{
				Name = "Testo Teppo",
				Email = "Testimies@testing.com",
				Password = "1234",
			};
			User user = DatabaseManipulator.Save<User>("User", testi);
			testi.Name = "Matti Nykänen";
			user = DatabaseManipulator.Save<User>("User", testi);
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
