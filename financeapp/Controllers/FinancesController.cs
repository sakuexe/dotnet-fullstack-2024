using System.Diagnostics;
using System.Text.Json;
using financeapp.Data;
using financeapp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace financeapp.Controllers;

[Authorize]
public class FinancesController : Controller
{
    private readonly FinancesContext _context;
    public FinancesController(FinancesContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult Index()
    {
        // TODO: send a partial view with the expenses of the user
        var username = User.Identity?.Name;
        using var context = _context;
        var expenses = context.Finances.Where(f => f.User.Username == username).ToList();
        if (expenses.Count < 1)
        {
            return PartialView("_NoExpenses");
        }
        return PartialView("_Expenses", expenses);
    }

    [HttpPost]
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
        // we are adding expenses, so the amount must be negative
        if (model.IsExpense)
            model.Amount = Math.Abs(model.Amount) * -1;
        // otherwise the amount is positive
        else
            model.Amount = Math.Abs(model.Amount);

        Console.WriteLine($"{model.Title}, {model.Description}, {model.Category}, {model.Icon}, {model.Amount}");
        bool success = model.SaveToDatabase(_context, User.Identity?.Name);
        if (success)
            return Ok();

        var dbErrors = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string>
            {
                {"ErrorMessage", "An error occurred while saving to the database."}
            }
        };
        // if saving to the database was not successful, return status code 500
        return StatusCode(500, JsonSerializer.Serialize(dbErrors));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        using var context = _context;
        var expense = context.Finances.FirstOrDefault(f => f.Id == id);
        if (expense == null)
        {
            return StatusCode(404, "Expense not found");
        }
        try
        {
            context.Finances.Remove(expense);
            context.SaveChanges();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            return StatusCode(500, "Internal Server Error: Could not delete expense");
        }
        return Ok();
    }
}

