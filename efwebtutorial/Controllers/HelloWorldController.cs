using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace efwebtutorial.Controllers;

public class HelloWorldController : Controller
{
	// 
	// GET: /HelloWorld/
	public IActionResult Index()
	{
		return View();
	}
	// GET: /HelloWorld/Welcome/ 
	// Requires using System.Text.Encodings.Web;
	public IActionResult Welcome(string name, int NumTimes = 1)
	{
		ViewData["Message"] = "Hello " + name;
		ViewData["NumTimes"] = NumTimes;
		return View();
	}
}