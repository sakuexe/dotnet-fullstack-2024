using System.ComponentModel.DataAnnotations;

namespace financeapp.Models;

// https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-model?view=aspnetcore-8.0&tabs=visual-studio-code
public class User
{
    public int Id { get; set; } // Primary Key, EF automatically makes it a PK and auto increments
    [Required]
    [MinLength(3)]
    [DataType(DataType.Text)]
    public string Username { get; set; }
    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    // Navigation Property - used for easy access to related data
    public List<Finance>? Finances { get; set; }
    public decimal? SavingsGoal { get; set; }
}
