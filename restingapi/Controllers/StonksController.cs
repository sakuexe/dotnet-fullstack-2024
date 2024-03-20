using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using restingapi.Models;

namespace restingapi.Controllers;

public class StonksController : Controller
{
    // for less fun, use the static version
    // private static readonly BusinessContainer _businessContainer = new();
    private readonly BusinessContainer _businessContainer = new();
	private readonly ILogger<HomeController> _logger;

	public StonksController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

	public IActionResult Index()
	{
		return View();
	}

    [HttpPost]
    public IActionResult _Stonks(string? businessName)
    {
        businessName ??= "Apple";
        var business = _businessContainer.GetBusiness(businessName);
        var businessJson = JsonSerializer.Serialize(business);
        return Content(businessJson, "application/json");
    }

    [HttpGet]
	public IActionResult _Businesses()
	{
        _businessContainer.UpdateStocks();
		return PartialView(_businessContainer.Businesses);
	}
}
