using System.Text.Json;
using financeapp.Data;
using financeapp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace financeapp.Controllers;

[Authorize]
public class SavingsController : Controller
{
    private readonly FinancesContext _context;
    public SavingsController(FinancesContext context)
    {
        _context = context;
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


    [HttpPost]
    public IActionResult Index()
    {
        // gets the savings for the current month
        // returns in json format with Content-Type: application/json
        var username = User.Identity?.Name;

        var savingsDelta = new SavingsDelta();
        var finances = _context.Finances.Where(f => f.User.Username == username)
            .Where(f => f.CreatedAt.Month == DateTime.Now.Month)
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

            if (savingsDelta.totalByDays != null)
            {
                savingsDelta.totalByDays.Add(dayTotal); continue;
            }
            // if the list is null, create a new list and add the dayTotal
            savingsDelta.totalByDays = new List<DayTotal>();
            savingsDelta.totalByDays.Add(dayTotal);
        }

        var savingsGoal = _context.Users.Where(u => u.Username == username).FirstOrDefault()?.SavingsGoal ?? 0;
        savingsDelta.savingsGoal = savingsGoal;

        var JSON = JsonSerializer.Serialize(savingsDelta);
        return Content(JSON, "application/json");
    }
}
