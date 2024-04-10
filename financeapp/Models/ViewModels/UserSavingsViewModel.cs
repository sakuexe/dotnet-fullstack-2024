using System.ComponentModel.DataAnnotations;
using financeapp.Data;

namespace financeapp.Models.ViewModels;

public class UserSavingsViewModel
{
    [Required]
    public int SavingsGoal { get; set; }
    private FinancesContext Context { get; set; }

    public UserSavingsViewModel() { }

    public UserSavingsViewModel(FinancesContext context)
    {
        Context = context;
    }

    public int GetSavingsGoal(string username)
    {
        var user = Context.Users.Where(u => u.Username == username).FirstOrDefault();
        return user?.SavingsGoal ?? 0;
    }
}
