using System.Text.Json;
using financeapp.Data;

namespace financeapp.Models.ViewModels;

public class DashboardViewModel
{
    public List<Finance> Finances { get; set; }
    public List<string> Colors { get; set; }
    public FinancesContext Context { get; set; }
    public UserSavingsViewModel Savings { get; set; }

    public struct CategoryTotal
    {
        public string Category { get; set; }
        public decimal Total { get; set; }
        public string Icon { get; set; }
    }

    public List<CategoryTotal> GetTopCategories(int limit = 4)
    {
        var categories = Finances
            .GroupBy(f => f.Category)
            .Select(g => new CategoryTotal
            {
                Category = g.Key,
                Total = g.Sum(f => (decimal)f.AmountCents / 100),
                Icon = g.First().Icon ?? string.Empty
            })
            .Where(g => g.Total < 0)
            .Take(limit)
            .OrderBy(g => g.Total)
            .ToList();
        return categories;
    }

    public string PercentageOfTotal(string category)
    {
        var totalExpenses = Finances
            .Where(f => f.AmountCents < 0)
            .Sum(f => f.AmountCents);
        var categoryTotal = Finances
            .GroupBy(f => f.Category)
            .SelectMany(g => g.Where(f => f.Category == category))
            .Sum(f => f.AmountCents);

        var percentage = ((double)categoryTotal / (double)totalExpenses) * 100;
        return percentage.ToString("0.00");
    }

    public DashboardViewModel(FinancesContext _context, string username)
    {
        // get the context for the database
        Context = _context;
        Savings = new UserSavingsViewModel(_context);
        // get colors for the expenses from a json file
        var colorsJson = File.ReadAllText("wwwroot/expensecolors.json");
        Colors = JsonSerializer.Deserialize<List<string>>(colorsJson) ??
           throw new Exception("expensecolors.json could not be desearialized.");
        var user = _context.Users.Where(u => u.Username == username).FirstOrDefault();
        if (user == null)
        {
            Finances = new List<Finance>();
            return;
        }
        Finances = _context.Finances.Where(f => f.UserId == user.Id).ToList();
    }
}
