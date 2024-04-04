using System.Text.Json;
using financeapp.Data;
using financeapp.Models;
using financeapp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace financeapp.Controllers;

public class FinanceController : Controller
{
    private readonly FinancesContext _context;
    public FinanceController(FinancesContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(NewExpenseViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // TODO: return error with view
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            // send json response with errors, use status code 400
            return BadRequest(JsonSerializer.Serialize(errors));
        }
        Console.WriteLine($"{model.Title}, {model.Description}, {model.Category}, {model.Icon}, {model.AmountCents}");
        // add the finance object to the database
        var result = new Dictionary<string, string> {
            { "status", "success" }
        };
        return Content(JsonSerializer.Serialize(result), "application/json");
    }

}

