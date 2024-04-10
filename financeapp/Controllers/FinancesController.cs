using System.Diagnostics;
using System.Text.Json;
using financeapp.Data;
using financeapp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        var expenses = context.Finances.
            Where(f => f.User.Username == username)
            .OrderByDescending(f => f.CreatedAt)
            .ToList();
        if (expenses.Count < 1)
        {
            return PartialView("_NoExpenses");
        }
        return PartialView("_Expenses", expenses);
    }

    [HttpPost]
    public IActionResult PieChartData()
    {
        var username = User.Identity?.Name;
        using var context = _context;
        var expenses = context.Finances.Where(f => f.User.Username == username).ToList();
        if (expenses.Count < 1)
        {
            return BadRequest("No expenses found");
        }
        // get the categories and the total amount spent on each category
        var categories = expenses
            .GroupBy(f => f.Category)
            .Select(g => new { Category = g.Key, Amount = g.Sum(f => f.AmountCents) })
            .OrderBy(c => c.Amount)
            .ToList();
        return Content(JsonSerializer.Serialize(categories));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(NewExpenseViewModel model)
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
        bool success = await model.SaveToDatabase(_context, User.Identity?.Name);
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
    public async Task<IActionResult> Delete(int id)
    {
        using var context = _context;
        var expense = await context.Finances.FirstOrDefaultAsync(f => f.Id == id);
        if (expense == null)
        {
            return StatusCode(404, "Expense not found");
        }
        try
        {
            context.Finances.Remove(expense);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            return StatusCode(500, "Internal Server Error: Could not delete expense");
        }
        return Ok();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateSavings(UserSavingsViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return BadRequest(JsonSerializer.Serialize(errors));
        }

        var username = User.Identity?.Name;
        var user = _context.Users.Where(u => u.Username == username).FirstOrDefault();
        if (user == null)
            return BadRequest("User not found");

        user.SavingsGoal = model.SavingsGoal;
        _context.SaveChanges();
        return Ok();
    }

    private struct DayTotal
    {
        public DateOnly Date { get; set; }
        public decimal Total { get; set; }
    }

    private struct SavingsDelta
    {
        public List<DayTotal> totalByDays { get; set; }
        public decimal savingsGoal { get; set; }
    }

    [HttpPost]
    public IActionResult GetSavings()
    {
        var username = User.Identity?.Name;

        var savingsDelta = new SavingsDelta();
        var finances = _context.Finances.Where(f => f.User.Username == username)
            .OrderBy(f => f.CreatedAt.Date)
            .ToList();
        int balance = 0;

        // calculate the balance for each day
        foreach (var day in finances.GroupBy(f => f.CreatedAt.Date))
        {
            balance += day.Sum(f => f.AmountCents);
            var dayTotal = new DayTotal
            {
                Date = DateOnly.FromDateTime(day.Key),
                Total = balance
            };

            if (savingsDelta.totalByDays == null)
                savingsDelta.totalByDays = new List<DayTotal>();
            else
                savingsDelta.totalByDays.Add(dayTotal);
        }

        var user = _context.Users.Where(u => u.Username == username).FirstOrDefault();
        if (user == null)
            return BadRequest("User not found");
        savingsDelta.savingsGoal = user.SavingsGoal ?? 0;

        var JSON = JsonSerializer.Serialize(savingsDelta);
        return Content(JSON, "application/json");
    }
}

