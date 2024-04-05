using System.Text.Json;
using financeapp.Data;
using financeapp.Models;
using financeapp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [ValidateAntiForgeryToken]
    public IActionResult Add(NewExpenseViewModel model)
    {
        // The NewExpenseViewModel is has the amount as a double
        // The Finance model has the amount in cents as an integer
        // So the amount needs to be converted to cents before saving to the database
        if (!ModelState.IsValid)
        {
            // TODO: return error with view
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            // send json response with errors, use status code 400
            return BadRequest(JsonSerializer.Serialize(errors));
        }
        Console.WriteLine($"{model.Title}, {model.Description}, {model.Category}, {model.Icon}, {model.Amount}");
        bool success = model.SaveToDatabase(_context, User.Identity?.Name);
        // TODO: if not success
        // add the finance object to the database
        return Ok();
    }

}

