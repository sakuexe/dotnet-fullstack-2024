using System.Text.Json;
using forming.Models;
using Microsoft.AspNetCore.Mvc;

namespace forming.Controllers
{
	public class PartialController : Controller
	{
		[HttpGet]
		public IActionResult Index(int number)
		{
			ViewData["number"] = number;
			return View();
		}

		[HttpPost]
		public IActionResult _FirstPage(int number)
		{
			ViewData["number"] = number;
			return PartialView();
		}

		[HttpPost]
		public IActionResult _SecondPage()
		{
			return PartialView();
		}
		[HttpPost]
		public IActionResult _ThirdPage(int number)
		{
			var bobs = new List<Bob>();
			for (int i = 0; i < number; i++)
				bobs.Add(new Bob());
			var json = JsonSerializer.Serialize(bobs);
			ViewData["jsonbobs"] = json;
			return PartialView();
		}

		[HttpPost]
		public string RecieveData(string package)
		{
			try
			{
				var bobList = JsonSerializer.Deserialize<List<Bob>>(package);
				return "Great success!";
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}
	}
}