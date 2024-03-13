using helloworld.Models;
using helloworld.Models.Utils;
using Microsoft.AspNetCore.Mvc;

namespace helloworld.Controllers
{
	public class WorkController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			var work = new WorkOrderModel();
			return View(work);
		}
	}
}
