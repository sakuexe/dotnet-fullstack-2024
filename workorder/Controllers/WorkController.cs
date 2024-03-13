using Microsoft.AspNetCore.Mvc;
using workorder.Models;

namespace workorder.Controllers
{
    public class WorkController : Controller
    {
        [HttpGet]
        public IActionResult Index(int amount)
        {
            // default to 5 workers if no amount is given
            amount = amount > 0 ? amount : 5;

            var currentWork = new WorkOrderViewModel();
            // add random workers to the work
            foreach (var i in Enumerable.Range(0, amount))
            {
                currentWork.workers.Add(new Worker());
            }
            // add a security guard to make sure that no kepponen is played
            currentWork.workers.Add(new Worker("Teppo Tarkkailija", Worker.Title.security));
            return View(currentWork);
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
