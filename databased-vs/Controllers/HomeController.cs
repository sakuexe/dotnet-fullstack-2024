using databased_vs.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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

        public IActionResult UserDetails(string property, string value, bool multiple = false)
        {
            if (string.IsNullOrEmpty(property) || string.IsNullOrEmpty(value))
            {
                return RedirectToAction("Index", new { error = "Silly silly boy" });
            }
            var user = DatabaseManipulator.FindOne<User>(property, value);
            return View(user);
        }

        public IActionResult ManyUsers(string property, string value)
        {
            if (string.IsNullOrEmpty(property) || string.IsNullOrEmpty(value))
            {
                return RedirectToAction("Index", new { error = "Silly silly boy" });
            }
            var users = DatabaseManipulator.FindMany<User>(property, value);
            return View(users);
        }

        public IActionResult AddUser(bool safepassword = false)
        {

            User newUser = new();
            DatabaseManipulator.Save(newUser); // save the user
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult HidePassword(string id)
        {
            // get the user from the database with the given id
            var user = DatabaseManipulator.GetByObjectId<User>("User", new ObjectId(id));

            user.Password = $"{user.Password.Substring(0, 2)}{new string('*', user.Password.Length - 2)}";
            DatabaseManipulator.Save(user); // use the same save function to update the record

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
