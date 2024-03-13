using helloworld.Models;
using helloworld.Models.Utils;
using Microsoft.AspNetCore.Mvc;

namespace helloworld.Controllers
{
    public class MoneyController : Controller
    {
        [HttpGet]
        public IActionResult Index(int amount)
        {
            // using the personator static class
            var personList = new List<TestPersonModel>();
            // for loops with one line per iteration do not
            // need special brackets, reducing clutter
            for (int i = 0; i < amount; i++)
                personList.Add(Personator.CreatePerson());
            var ret = new MoneyViewModel(personList);
            return View(ret);
        }
        [HttpGet]
        [Route("Stonks")]
        // parameters given to the controller will be used as query parameters
        // for httpget these are in the form of ?key=value
        public IActionResult SendMoney(int amount)
        {
            // var infers the type of the variable from the assignment
            var moneyMultiplier = 1.2;
            var newMoney = moneyMultiplier * (int)amount;
            return View(newMoney);
        }
    }
}
