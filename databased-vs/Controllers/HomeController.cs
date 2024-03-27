using databased_vs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace databased_vs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly List<string> _names = new List<string>(){
            "Pertti", "Paavo", "Maksimus", "Saku", "Sienimies", "Amogustus",
            "Jeso'n", "Patrik", "Frank", "Matt", "Homer",
        };

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
            var rnd = new Random();
            var randomName = _names[rnd.Next(0, _names.Count)];
            /* var testi = new User() */
            /* { */
            /*     Name = randomName, */
            /*     Email = $"{randomName}{rnd.Next(100)}@testing.com", */
            /*     Password = $"pswrd_{rnd.Next(1000)}_{rnd.Next(200)}#", */
            /* }; */
            User testi = new()
            {
                Name = "sakuk",
                Email = "susamogus",
                Password = "safeword",
            };
            User user = DatabaseManipulator.Save<User>(testi);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
