using System.ComponentModel.DataAnnotations;
using financeapp.Data;

namespace financeapp.Models.ViewModels;

public class NewExpenseViewModel
{
    [Required]
    [MaxLength(128)]
    public string Title { get; set; }
    [MaxLength(512)]
    public string? Description { get; set; }
    [Required]
    [MaxLength(48)]
    public string Category { get; set; }
    [MaxLength(2)]
    public string? Icon { get; set; }
    [Required]
    [Range(0.05, double.MaxValue, ErrorMessage = "Amount must be greater than 5 cents.")]
    public double Amount { get; set; }
    // this helps the UX, so that the user does not have to select the type of expense
    // we also don't need another form, since we can just check this property
    public bool IsExpense { get; set; } = true;
    private readonly FinancesContext _context;

    public NewExpenseViewModel(FinancesContext context)
    {
        _context = context;
    }
    

    public Dictionary<string, string> GetCategories(string username)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        // get the categories and icons for the categories
        var categories = _context.Finances
            .Where(f => f.User == user)
            .Select(f => new { f.Category, f.Icon })
            .Distinct()
            .ToDictionary(f => f.Category, f => f.Icon!);
        return categories;
    }

    // return true if the expense was saved to the database
    // return false otherwise
    public async Task<bool> SaveToDatabase(FinancesContext context, string? username)
    {
        // get the user from the username, the usernames are unique
        var user = context.Users.FirstOrDefault(u => u.Username == username);
        if (user == null){
            Console.WriteLine("User not found");
            return false;
        }
        // convert the amount to cents
        var amountInCents = (int)(Math.Round(Amount, 2) * 100);
        var expense = new Finance
        {
            Title = Title,
            Description = Description,
            Category = Category,
            Icon = Icon ?? Category.Substring(0, 2),
            AmountCents = amountInCents,
            User = user
        };
        try {
            using var transaction = context;
            await transaction.Finances.AddAsync(expense);
            await transaction.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
        return true;
    }
}
