using helloworld.Models;
using Microsoft.AspNetCore.Mvc;

namespace helloworld.Controllers
{
	public class WorkController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			var work = new WorkOrderModel();
            work.Workers.Add(new Worker());
			return View(work);
		}
	}
}
