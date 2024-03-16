using Microsoft.AspNetCore.Mvc;
using workorder.Models;

namespace workorder.Controllers
{
    public class WorkController : Controller
    {
        [HttpGet]
        public IActionResult Index(int amount)
        {
            // if the amount of work orders is not given, default to 5
            amount = amount > 0 ? amount : 5;

            var workOrders = new List<WorkOrderViewModel>();

            foreach (var i in Enumerable.Range(0, amount))
            {
                workOrders.Add(new WorkOrderViewModel());
            }

            return View(workOrders);
        }
        [HttpGet]
        [Route("test")]
        // parameters given to the controller will be used as query parameters
        // for httpget these are in the form of ?key=value
        public IActionResult SendMoney(string message)
        {
            return View(message ?? "No message given");
        }
    }
}
