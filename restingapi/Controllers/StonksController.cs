using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using restingapi.Models;

namespace restingapi.Controllers;

public class StonksController : Controller
{
    private static readonly BusinessContainer _businessContainer = new();
	private readonly ILogger<HomeController> _logger;

	public StonksController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult UpdateStocks()
    {
        _businessContainer.UpdateStocks();
        // redirect to Home/index
        return RedirectToAction("Index", "Home");
    }

	public IActionResult Index()
	{
		return View();
	}

    [HttpPost]
    public IActionResult _Stonks(string? businessName)
    {
        // Get the stonks and information for a single business
        // returns a json object for the business
        businessName ??= "Apple"; // default business name
        var business = _businessContainer.GetBusiness(businessName);
        var businessJson = JsonSerializer.Serialize(business);
        return Content(businessJson, "application/json");
    }

    [HttpGet]
	public IActionResult _Businesses()
	{
        // get the list of businesses and all their information
        // returns a partial view, ready for use in the main page
        _businessContainer.UpdateStocks();
		return PartialView(_businessContainer.Businesses);
	}
}
